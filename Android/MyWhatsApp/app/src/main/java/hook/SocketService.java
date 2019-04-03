package hook;

import android.Manifest;
import android.app.Service;
import android.content.Context;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.database.Cursor;
import android.os.IBinder;
import android.os.Looper;
import android.provider.ContactsContract;
import android.provider.Settings;
import android.support.annotation.Nullable;
import android.support.v4.app.ActivityCompat;
import android.telephony.TelephonyManager;
import android.util.Log;

import com.example.administrator.my.MainActivity;
import com.google.gson.Gson;

import org.json.JSONObject;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.io.PrintWriter;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.ArrayList;
import java.util.HashSet;
import java.util.List;
import java.util.Set;

import hook.bean.MessageJson;
import hook.util.WriteUtil;

/**
 * Created by Administrator on 2018/7/3.
 */

public class SocketService extends Service {

    private static Socket socket = null;
    public static String imei = "";
    public static String androidID = "";
    public static MainActivity activity = null;
    static Gson gson = new Gson();
    public static SocketService socketService;
    public static Context service_Context;

    @Override
    public void onCreate() {
        super.onCreate();
        socketService=this;
        service_Context=getApplication();
        Log.e("LZC","onCreate:"+imei);
        //获取Imei号
        try {
            TelephonyManager telephonyManager = (TelephonyManager) this.getApplicationContext().getSystemService(this.getApplicationContext().TELEPHONY_SERVICE);
            if (ActivityCompat.checkSelfPermission(this, Manifest.permission.READ_PHONE_STATE) != PackageManager.PERMISSION_GRANTED) {
                return;
            }
            imei = telephonyManager.getDeviceId();
            Log.e("LZC","imei22222:"+imei);
        } catch (Exception e) {
            e.printStackTrace();
        }
        //获取安卓id
        String ANDROID_ID = Settings.System.getString(this.getApplicationContext().getContentResolver(), Settings.System.ANDROID_ID);
        Log.e("LZC","ANDROID_ID:"+ANDROID_ID);
        androidID=ANDROID_ID;

//        new Thread(new Runnable() {
//            @Override
//            public void run() {
//                try {
//                    ServerSocket serverSocket = new ServerSocket(22222);
//                    Log.e("LZC","111111");
//                    while (true) {
//                        Socket socket = serverSocket.accept();
//                        Log.e("LZC","2222");
//                        acceptConnect(socket);
//                    }
//                } catch (IOException e) {
//                    e.printStackTrace();
//                }
//            }
//        }).start();

//        try {
//            Thread.sleep(5000);
//        } catch (InterruptedException e) {
//            e.printStackTrace();
//        }
        //创建stock链接
        new Thread(new Runnable() {
            @Override
            public void run() {
                CreateConnection();
            }
        }).start();
    }

    @Override
    public int onStartCommand(Intent intent, int flags, int startId) {
        Log.e("LZC","onStartCommand");

        return super.onStartCommand(intent, flags, startId);
    }

    @Override
    public IBinder onBind(Intent intent) {
        return null;
    }


    /**
     * 创建客服端连接
     */
    public static void CreateConnection() {
        try {
            //1、创建客户端Socket，指定服务器地址和端口
            if (socket == null) {
//                socket =new Socket("127.0.0.1",22222);
                //socket =new Socket("192.168.1.101",22222);
//                Log.e("LZC", "开始链接!!!!");
                /**** 通过imei获取宿主Ip ****/
//                MyHelper.SetString("imei",imei);
//                int a=Integer.valueOf(imei.substring(0,3));
//                int b=Integer.valueOf(imei.substring(3,6));
//                int c=Integer.valueOf(imei.substring(6,9));
//                int d=Integer.valueOf(imei.substring(9,12));
//                String ip=a+"."+b+"."+c+"."+d;
//                MyHelper.SetString("ip",ip);
//                socket =new Socket(ip,22222);
                /**** 通过安卓id获取宿主Ip ****/
                MyHelper.SetString("androidId",androidID);

                int a=Integer.valueOf(androidID.substring(0,3));
                int b=Integer.valueOf(androidID.substring(3,6));
                int c=Integer.valueOf(androidID.substring(6,9));
                int d=Integer.valueOf(androidID.substring(9,12));
                String ip=a+"."+b+"."+c+"."+d;
                MyHelper.SetString("ip",ip);

                socket =new Socket(ip,22222);
                Log.e("LZC", "socket：" + socket);
            }

            Log.e("LZC", "socket1111：" + socket);
            //2、获取输出流，向服务器端发送信息
            OutputStream os = socket.getOutputStream();//字节输出流
            PrintWriter pw = new PrintWriter(os);//将输出流包装成打印流

            //像服务器上传imei号
            JSONObject jsonObject =new JSONObject();
            jsonObject.put("action",0);
            jsonObject.put("tasknum",0);
            jsonObject.put("context",androidID);
            Log.e("LZC","jsonObject:"+jsonObject.toString());
            WriteUtil.write(socket,jsonObject.toString());
            Log.e("LZC", "发送！！");
            //3、获取输入流，并读取服务器端的响应信息
            while (true) {
                BufferedReader br = new BufferedReader(new InputStreamReader(socket.getInputStream(),"UTF-8"));
                String info = null;
                info = br.readLine();
                Log.e("LZC", "收到："+info);
                if (info == null) {
                    MyHelper.appendString("serverIn","服务器："+info);
                    return;
                }else{
                    //发送服务器指令
                    try {
                        Task.ReceiveTask(info);
                    } catch (Exception e) {
                        e.printStackTrace();
                    }
                }
            }
        } catch (Exception e) {
            Log.e("LZC", "can not listen to:" + e);// 出错，打印出错信息
        }
    }


    /**
     * 服务端
     * @param socket
     */
    private void acceptConnect(final Socket socket) {

        System.out.println("accepted...");
        new Thread(new Runnable() {
            @Override
            public void run() {
                Log.e("LZC", "readMsg");
                Looper.prepare();
                readMsg(socket);
            }
        }).start();
    }

    /**
     * 读取stock
     *
     * @param socket
     */
    private void readMsg(final Socket socket) {

        while (true) {
//            Log.e("LZC", "111111");
            try {
                BufferedReader reader = null;
                reader = new BufferedReader(new InputStreamReader(socket.getInputStream()));

                String msg = readBuffer(reader);
                if (msg == null) {
                    return;
                }else{
                    //2、获取输出流，向服务器端发送信息
                    OutputStream os = socket.getOutputStream();//字节输出流
                    Log.e("LZC", "os：" + os);
                    PrintWriter pw = new PrintWriter(os);//将输出流包装成打印流
                    Log.e("LZC", "pw：" + pw);

                    hook.bean.Context context=new hook.bean.Context();
                    context.setTasktype(1);
                    context.setTxtmsg("发送视频");

                    List list=new ArrayList();
                    list.add("storage/emulated/0/123.mp4");
                    context.setList(list);

                    MessageJson messageJson =new MessageJson();
                    messageJson.setAction(3);
                    messageJson.setTasknum(123);
                    messageJson.setContext(context);

                    String message=gson.toJson(messageJson);

                    pw.write(message);
                    pw.write("\r\n");
                    pw.flush();
                }
            } catch (IOException e) {
                e.printStackTrace();
            }
        }
    }

    /**
     * 读取IO流
     *
     * @param reader
     * @return
     * @throws IOException
     */
    public static String readBuffer(BufferedReader reader) throws IOException {
        Log.e("LZC", "reader:" + reader);
        String msg;
        try {
            msg = reader.readLine();
            Log.e("LZC", "msg222222:" + msg);
            if (msg == null) {
                return null;
            }
        } catch (Exception e) {
            Log.e("LZC", "异常:" + e.getMessage());
            e.printStackTrace();
            return null;
        }
        return msg;
    }



}
