package hook;

import android.content.Intent;
import android.util.ArraySet;
import android.util.Log;

import com.example.administrator.my.MainActivity;
import com.google.gson.Gson;

import java.util.ArrayList;
import java.util.Date;
import java.util.HashSet;
import java.util.List;
import java.util.Set;

import hook.bean.MessageJson;
import hook.util.AddressBookUtils;

/**
 * Created by DH on 2018/6/13.
 */

public class Task {
    static Gson gson = new Gson();

    public static void ReceiveTask(String str) {
        Log.e("LZC", "我是客户端，服务器说：" + str);
        MyHelper.appendString("serverIn", new Date() + "===服务器====" + str);
        MessageJson message = gson.fromJson(str, MessageJson.class);

        int taskType = message.getContext().getTasktype();
        final String txtmsg = message.getContext().getTxtmsg();
        final List<String> list = message.getContext().getList();
        Log.e("LZC","接收到：type:"+taskType);
        Log.e("LZC","接收到：txtmsg:"+txtmsg);
        Log.e("LZC","接收到：phonePath:"+list.size());

        switch (taskType) {
            case 1://导入通讯录
                Intent intent1 = new Intent("MyWhatsApp");
                intent1.putExtra("act", "ImportAddress");
                intent1.putExtra("phonePath", list.get(0));
                SocketService.service_Context.sendBroadcast(intent1);
                break;
            case 13://清除通讯录
                Intent intent12 = new Intent("MyWhatsApp");
                intent12.putExtra("act", "clearPhone");
                SocketService.service_Context.sendBroadcast(intent12);
                break;
            case 2://文字动态
                Log.e("LZC","接收到：文字动态:");
                Intent intent2 = new Intent("MyWhatsApp");
                intent2.putExtra("act", "trackText");
                intent2.putExtra("text", txtmsg);
                SocketService.service_Context.sendBroadcast(intent2);
                break;
            case 3://图片动态
                for (int i = 0; i < list.size(); i++) {
                    Intent intent3 = new Intent("MyWhatsApp");
                    intent3.putExtra("act", "trackImg");
                    intent3.putExtra("imgPath", list.get(i));
                    intent3.putExtra("text", "");
                    SocketService.service_Context.sendBroadcast(intent3);
                }
                break;
            case 4://图片配文字动态
                for (int i = 0; i < list.size(); i++) {
                    Intent intent4 = new Intent("MyWhatsApp");
                    intent4.putExtra("act", "trackImg");
                    intent4.putExtra("imgPath", list.get(i));
                    if(i==0){
                        intent4.putExtra("text", txtmsg);
                    }else{
                        intent4.putExtra("text", "");
                    }
                    SocketService.service_Context.sendBroadcast(intent4);
                }
                break;
            case 5://发送文字消息
                Intent intent5 = new Intent("MyWhatsApp");
                intent5.putExtra("act", "sendText");
                intent5.putExtra("text", txtmsg);
                SocketService.service_Context.sendBroadcast(intent5);
                break;
            case 6://发送图片消息
                for (int i = 0; i < list.size(); i++) {
                    Intent intent6 = new Intent("MyWhatsApp");
                    intent6.putExtra("act", "sendImg");
                    intent6.putExtra("imgPath", list.get(i));
                    intent6.putExtra("text", "");
                    SocketService.service_Context.sendBroadcast(intent6);
                }
                break;
            case 7://发送图文消息
                for (int i = 0; i < list.size(); i++) {
                    Intent intent7 = new Intent("MyWhatsApp");
                    intent7.putExtra("act", "sendImg");
                    intent7.putExtra("imgPath", list.get(i).toString());
                    if (i == 0) {
                        intent7.putExtra("text", txtmsg);
                    } else {
                        intent7.putExtra("text", "");
                    }
                    SocketService.service_Context.sendBroadcast(intent7);
                }
                break;
            case 8://发送视频消息
                for (int i = 0; i < list.size(); i++) {
                    Intent intent8 = new Intent("MyWhatsApp");
                    intent8.putExtra("act", "sendVideo");
                    intent8.putExtra("videoPath", list.get(i).toString());
                    intent8.putExtra("text", "");
                    SocketService.service_Context.sendBroadcast(intent8);
                }
                break;
            case 9://发送视文消息
                for (int i = 0; i < list.size(); i++) {
                    Intent intent8 = new Intent("MyWhatsApp");
                    intent8.putExtra("act", "sendVideo");
                    intent8.putExtra("videoPath", list.get(i).toString());
                    if (i == 0) {
                        intent8.putExtra("text", txtmsg);
                    } else {
                        intent8.putExtra("text", "");
                    }
                    SocketService.service_Context.sendBroadcast(intent8);
                }
                break;
        }
    }
}
