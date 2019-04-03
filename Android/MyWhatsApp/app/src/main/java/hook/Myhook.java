package hook;
import android.app.Activity;
import android.app.WallpaperManager;
import android.content.BroadcastReceiver;
import android.content.Intent;
import android.content.IntentFilter;
import android.content.pm.PackageManager;
import android.graphics.BitmapFactory;
import android.os.Bundle;
import android.util.Log;
import android.widget.Button;

import org.json.JSONObject;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.io.PrintWriter;
import java.net.Socket;

import de.robv.android.xposed.IXposedHookLoadPackage;
import de.robv.android.xposed.XC_MethodHook;
import de.robv.android.xposed.XposedHelpers;
import de.robv.android.xposed.callbacks.XC_LoadPackage;
import hook.util.WriteUtil;

import static de.robv.android.xposed.XposedBridge.log;
import static de.robv.android.xposed.XposedHelpers.findAndHookMethod;

/**
 * Created by DH on 2018/5/16.
 */

public class Myhook implements IXposedHookLoadPackage {
    public static Activity HomeActivity = null;
    public static XC_LoadPackage.LoadPackageParam lpp = null;
    private static BroadcastReceiver mReceiver = null;
    private static Socket socket = null;
    public static String imei;
    public static Button registerPhone_button=null;
    public static Button EULA_button=null;
    @Override
    public void handleLoadPackage(final XC_LoadPackage.LoadPackageParam loadPackageParam) throws Throwable {

//        XposedHelpers.findAndHookMethod(TelephonyManager.class.getName(), loadPackageParam.classLoader, "getDeviceId", new Object[] { new XC_MethodHook() {
//            @Override
//            protected void afterHookedMethod(MethodHookParam param) throws Throwable {
//                //将返回得imei值设置为我想要的值
//                Log.e("LZC","劫持结束了~"+param.getResult().toString());
//                imei=param.getResult().toString();
////                param.setResult("1234567890");
//
//            }
//        } });


        if(loadPackageParam.packageName.indexOf("com.whatsapp") > -1){
            Log.e("LZC","开始HOOK！！！！");

            //自动登录
            AutoLogin(loadPackageParam);

            //HOOK whatsAPP 主界面
            findAndHookMethod(loadPackageParam.classLoader.loadClass("com.whatsapp.HomeActivity"), "onCreate", Bundle.class, new XC_MethodHook() {
                protected void afterHookedMethod(final MethodHookParam paramAnonymousMethodHookParam)
                        throws Throwable {
                    HomeActivity = (Activity) paramAnonymousMethodHookParam.thisObject;
                    lpp = loadPackageParam;
                    Log.e("LZC","Hook到whatsapp了！！");
                    log("Hook到whatsapp了！！");
                    IntentFilter filter = new IntentFilter("MyWhatsApp");
                    mReceiver = new AdbReceiver();
                    HomeActivity.registerReceiver(mReceiver, filter);
//                    new Thread(new Runnable() {
//                        @Override
//                        public void run() {
//                            CreateConnection();
//                        }
//                    }).start();
                }
            });
        }

        if(loadPackageParam.packageName.indexOf("com.android.launcher3") > -1){
            findAndHookMethod(loadPackageParam.classLoader.loadClass("com.android.launcher3.Launcher"), "onCreate", Bundle.class, new XC_MethodHook() {
                protected void afterHookedMethod(final MethodHookParam param)
                        throws Throwable {
                    Log.e("LZC","进入开机首页！！");
                    Activity activity= (Activity) param.thisObject;
                    WallpaperManager wallpaperManager = WallpaperManager.getInstance(activity);
                    try {
                        Log.e("LZC","设置背景");
                        String background=MyHelper.GetString("background.txt","");
                        Log.e("LZC","background:"+background);
                        wallpaperManager.setBitmap(BitmapFactory.decodeFile(background));
                        Log.e("LZC","设置背景成功");
                    } catch (IOException e) {
                        Log.e("LZC", "设置背景失败："+e.getMessage());
                        e.printStackTrace();
                    }
                    PackageManager packageManager = activity.getPackageManager();
                    Intent intent = new Intent();
                    intent = packageManager.getLaunchIntentForPackage("com.example.dh.whatsappxp");
                    activity.getApplication().startActivity(intent);
                }
            });
        }
    }

    /**
     * 自动登录
     */
    public static void AutoLogin(final XC_LoadPackage.LoadPackageParam loadPackageParam) throws Exception{
        //同意并继续
        findAndHookMethod(loadPackageParam.classLoader.loadClass("com.whatsapp.registration.EULA"),
                "onCreate",
                Bundle.class,
                new XC_MethodHook() {
                    @Override
                    protected void afterHookedMethod(final MethodHookParam param)
                            throws Throwable {
                        final Activity EULA_activity= (Activity) param.thisObject;
                        IntentFilter filter = new IntentFilter("EULAAction");
                        ClickReceiver clickReceiver = new ClickReceiver();
                        EULA_activity.registerReceiver(clickReceiver, filter);
                        EULA_button=(Button) XposedHelpers.callMethod(param.thisObject,"findViewById",2131296806);
                        Log.e("LZC","EULA:"+"onCreate:"+"EULA_button:"+EULA_button);
                        new Thread(new Runnable() {
                            @Override
                            public void run() {
                                try {
                                    Thread.sleep(1000);
                                } catch (InterruptedException e) {
                                    e.printStackTrace();
                                }
                                Intent intent = new Intent("EULAAction");
                                intent.putExtra("act", "AgreeAndContinue");
                                EULA_activity.sendBroadcast(intent);
                            }
                        }).start();

                    }
                });

        //下一部
        findAndHookMethod(loadPackageParam.classLoader.loadClass("com.whatsapp.registration.RegisterPhone"),
                "onCreate",
                Bundle.class,
                new XC_MethodHook() {
                    @Override
                    protected void afterHookedMethod(final MethodHookParam param)
                            throws Throwable {
                        final Activity activity= (Activity) param.thisObject;
                        IntentFilter filter = new IntentFilter("RegisterPhoneAction");
                        NextStepReceiver clickReceiver = new NextStepReceiver();
                        activity.registerReceiver(clickReceiver, filter);
                        registerPhone_button=(Button) XposedHelpers.callMethod(param.thisObject,"findViewById",2131297493);
                        Log.e("LZC","RegisterPhone:"+"onCreate:"+"butt:"+registerPhone_button);
                        new Thread(new Runnable() {
                            @Override
                            public void run() {
                                try {
                                    Thread.sleep(1000);
                                } catch (InterruptedException e) {
                                    e.printStackTrace();
                                }
                                Log.e("LZC","nextStep:");
                                try {
                                    Intent intent = new Intent("RegisterPhoneAction");
                                    intent.putExtra("act", "NextStep");
                                    activity.sendBroadcast(intent);
                                } catch (Exception e) {
                                    Log.e("LZC","nextStep222:"+e.getMessage());
                                    e.printStackTrace();
                                }
                                Log.e("LZC","nextStep333:");
                            }
                        }).start();
                    }
                });

        //确定
        findAndHookMethod(loadPackageParam.classLoader.loadClass("com.whatsapp.registration.RegisterPhone"),
                "onCreateDialog",
                int.class,
                new XC_MethodHook() {
                    @Override
                    protected void afterHookedMethod(final MethodHookParam param)
                            throws Throwable {
                        Log.e("LZC","RegisterPhone【onCreateDialog】【参数1】"+param.args[0]);
                        if(Integer.parseInt(param.args[0].toString())==21){
                            Log.e("LZC","RegisterPhone【调用确定");
                            XposedHelpers.callMethod(param.thisObject,"n");
                        }
                    }
                });
    }
}
