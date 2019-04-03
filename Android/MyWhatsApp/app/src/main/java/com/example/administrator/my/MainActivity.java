package com.example.administrator.my;

import android.Manifest;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.database.Cursor;
import android.net.Uri;
import android.os.Bundle;
import android.os.Looper;
import android.provider.ContactsContract;
import android.provider.Settings;
import android.support.v4.app.ActivityCompat;
import android.support.v7.app.AppCompatActivity;
import android.telephony.TelephonyManager;
import android.util.Log;
import android.view.View;
import android.widget.Button;

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

import hook.MyHelper;
import hook.SocketService;
import hook.Task;
import hook.bean.MessageJson;
import hook.util.WriteUtil;

public class MainActivity extends AppCompatActivity {

    private static Socket socket = null;
    public static String imei="";
    public static String androidID="";
    public static MainActivity activity=null;
    static Gson gson = new Gson();

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        new Thread(new Runnable() {
            @Override
            public void run() {
                startService(new Intent(MainActivity.this, SocketService.class));
            }
        }).start();
        finish();
//
//        Button button = (Button) findViewById(R.id.send);
//        button.setOnClickListener(new View.OnClickListener() {
//            @Override
//            public void onClick(View v) {
//                /** 发送图片消息 **/
////                Intent intent = new Intent("MyWhatsApp");
////                intent.putExtra("act", "sendImg");
////                intent.putExtra("idphone", "8613628604872@s.whatsapp.net");
////                intent.putExtra("imgPath", "storage/emulated/0/b.jpg");
////                intent.putExtra("text", "发送图片!");
////                sendBroadcast(intent);
//
//                /** 发送图片动态 **/
////                Intent intent = new Intent("MyWhatsApp");
////                intent.putExtra("act", "sendImg");
////                intent.putExtra("idphone", "status@broadcast");
////                intent.putExtra("imgPath", "storage/emulated/0/b.jpg");
////                intent.putExtra("text", "哈哈哈!");
////                sendBroadcast(intent);
//
//                /** 发送视频**/
////                Intent intent = new Intent("MyWhatsApp");
////                intent.putExtra("act", "sendVideo");
////                intent.putExtra("idphone", "8613628604872@s.whatsapp.net");
////                intent.putExtra("videoPath", "storage/emulated/0/123.mp4");
////                intent.putExtra("text", "发送视频!");
////                sendBroadcast(intent);
//
//                /** 发送视频动态**/
////                Intent intent = new Intent("MyWhatsApp");
////                intent.putExtra("act", "sendVideo");
////                intent.putExtra("idphone", "status@broadcast");
////                intent.putExtra("videoPath", "storage/emulated/0/123.mp4");
////                intent.putExtra("text", "哈哈哈!");
////                sendBroadcast(intent);
//                new Thread(new Runnable() {
//                    @Override
//                    public void run() {
//                        CreateConnection();
////                        getPhoneSet();
////                        clearPhone();
//                    }
//                }).start();
//            }
//        });
//
//        activity=this;
//        try {
//            TelephonyManager telephonyManager = (TelephonyManager) this.getApplicationContext().getSystemService(this.getApplicationContext().TELEPHONY_SERVICE);
//            if (ActivityCompat.checkSelfPermission(this, Manifest.permission.READ_PHONE_STATE) != PackageManager.PERMISSION_GRANTED) {
//                // TODO: Consider calling
//                //    ActivityCompat#requestPermissions
//                // here to request the missing permissions, and then overriding
//                //   public void onRequestPermissionsResult(int requestCode, String[] permissions,
//                //                                          int[] grantResults)
//                // to handle the case where the user grants the permission. See the documentation
//                // for ActivityCompat#requestPermissions for more details.
//                return;
//            }
//            imei = telephonyManager.getDeviceId();
//            Log.e("LZC","imei22222:"+imei);
//        } catch (Exception e) {
//            e.printStackTrace();
//        }
//
//        String ANDROID_ID = Settings.System.getString(this.getApplicationContext().getContentResolver(), Settings.System.ANDROID_ID);
//        Log.e("LZC","ANDROID_ID:"+ANDROID_ID);
//        androidID=ANDROID_ID;
//
//        /**
//         * 创建服务端
//         */
//        new Thread(new Runnable() {
//            @Override
//            public void run() {
//                try {
//                    ServerSocket serverSocket = new ServerSocket(22222);
//                    Log.e("LZC","111111");
//                    while (true) {
//                        Socket socket = serverSocket.accept();
//                        Log.e("LZC","111111");
//                        acceptConnect(socket);
//                    }
//                } catch (IOException e) {
//                    e.printStackTrace();
//                }
//            }
//        }).start();
//
//
//        /**
//         * 导入通讯录
//         */
////        List<String> list=new ArrayList<>();
////        list.add("13129900133");
////        list.add("13125566544");
////        list.add("13124411255");
////        list.add("13120033698");
////        list.add("13129515133");
////
////        for (int i = 0; i < list.size(); i++) {
////            AddressBookUtils.saveContact(getApplicationContext(),list.get(i),AddressBookUtils.getName());
////        }

    }

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

    /**
     * 创建连接
     */
    public static void CreateConnection() {
        try {
            //1、创建客户端Socket，指定服务器地址和端口
            if (socket == null) {
//                socket =new Socket("127.0.0.1",22222);
//                socket =new Socket("192.168.2.212",22222);
//                Log.e("LZC", "开始链接!!!!");
            /**** 通过imei获取宿主Ip ****/
                MyHelper.SetString("imei",imei);
                int a=Integer.valueOf(imei.substring(0,3));
                int b=Integer.valueOf(imei.substring(3,6));
                int c=Integer.valueOf(imei.substring(6,9));
                int d=Integer.valueOf(imei.substring(9,12));
                String ip=a+"."+b+"."+c+"."+d;
                MyHelper.SetString("ip",ip);
                socket =new Socket(ip,22222);

                /**** 通过安卓id获取宿主Ip ****/
//                MyHelper.SetString("androidId",androidID);
//                socket =new Socket(androidID,22222);

                Log.e("LZC", "socket：" + socket);
            }

            Log.e("LZC", "socket1111：" + socket);
            //2、获取输出流，向服务器端发送信息
            OutputStream os = socket.getOutputStream();//字节输出流
            Log.e("LZC", "os：" + os);
            PrintWriter pw = new PrintWriter(os);//将输出流包装成打印流
            Log.e("LZC", "pw：" + pw);

            //像服务器上传imei号
            JSONObject jsonObject =new JSONObject();
            jsonObject.put("action",0);
            jsonObject.put("tasknum",0);
            jsonObject.put("context",imei);

            Log.e("LZC","jsonObject:"+jsonObject.toString());
            WriteUtil.write(socket,jsonObject.toString());

//            hook.bean.Context context=new hook.bean.Context();
//            context.setTasktype(5);
//            context.setTxtmsg("您好，海外就医就选康安途！");
//            context.setList(Arrays.asList("123456789"));
//
//            MessageJson messageJson =new MessageJson();
//            messageJson.setAction(3);
//            messageJson.setTasknum(123);
//            messageJson.setContext(context);
//
//            String message=gson.toJson(messageJson);

            pw.write("连接成功");
            pw.write("\r\n");
            pw.flush();
//            socket.shutdownOutput();//关闭输出流，socket仍然是连接状态
            Log.e("LZC", "发送！！");
            //3、获取输入流，并读取服务器端的响应信息
            while (true) {
                BufferedReader br = new BufferedReader(new InputStreamReader(socket.getInputStream(),"UTF-8"));
                String info = null;
                info = br.readLine();
                if (info == null) {
                    MyHelper.appendString("serverIn","服务器："+info);
                    return;
                }else{
                    //发送服务器指令
                    try {
//                        Task.ReceiveTask(info);
                    } catch (Exception e) {
                        e.printStackTrace();
                    }
                }
            }
        } catch (Exception e) {
            Log.e("LZC", "can not listen to:" + e);// 出错，打印出错信息
        }
    }


    public static void aaa() {
        try {
            // 1、创建客户端Socket，指定服务器地址和端口
            Socket socket = new Socket("127.0.0.1", 5209);
            Log.e("LZC", "客户端启动成功");
            // 2、获取输出流，向服务器端发送信息
            // 向本机的52000端口发出客户请求
            BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
            // 由系统标准输入设备构造BufferedReader对象
            PrintWriter write = new PrintWriter(socket.getOutputStream());
            // 由Socket对象得到输出流，并构造PrintWriter对象

            //3、获取输入流，并读取服务器端的响应信息
            BufferedReader in = new BufferedReader(new InputStreamReader(socket.getInputStream()));
            // 由Socket对象得到输入流，并构造相应的BufferedReader对象
            String readline;
            readline = br.readLine(); // 从系统标准输入读入一字符串
            while (!readline.equals("end")) {
                // 若从标准输入读入的字符串为 "end"则停止循环
                write.println(readline);
                // 将从系统标准输入读入的字符串输出到Server
                write.flush();
                // 刷新输出流，使Server马上收到该字符串
                System.out.println("Client:" + readline);
                // 在系统标准输出上打印读入的字符串
                System.out.println("Server:" + in.readLine());
                // 从Server读入一字符串，并打印到标准输出上
                readline = br.readLine(); // 从系统标准输入读入一字符串
            } // 继续循环
            //4、关闭资源
            write.close(); // 关闭Socket输出流
            in.close(); // 关闭Socket输入流
            socket.close(); // 关闭Socket
        } catch (Exception e) {
            System.out.println("can not listen to:" + e);// 出错，打印出错信息
        }
    }

    /**
     * 查找通讯录号码
     */
    public Set getPhoneSet() {
        Set<String> phoneSet = new HashSet<>();
        //使用ContentResolver查找联系人数据
        Cursor cursor = getContentResolver().query(ContactsContract.CommonDataKinds.Phone.CONTENT_URI, null, null, null, null);
        //遍历查询结果，
        if (cursor != null) {
            while (cursor.moveToNext()) {
                //获取联系人ID
                String phoneNum = cursor.getString(cursor.getColumnIndex(ContactsContract.CommonDataKinds.Phone.NUMBER));
                Log.e("LZC","phoneNum:"+phoneNum);
                phoneSet.add(phoneNum);
            }
            cursor.close();
        }
        return phoneSet;
    }

    /**
     * 清楚通讯录
     */
    public void clearPhone(){
        Uri uri = Uri.parse("content://com.android.contacts/raw_contacts");
        getContentResolver().delete(uri, "_id!=-1", null);
    }
}
