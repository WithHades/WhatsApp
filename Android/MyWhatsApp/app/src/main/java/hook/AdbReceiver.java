package hook;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.util.Log;

import java.util.Set;

import hook.util.AddressBookUtils;

public class AdbReceiver extends BroadcastReceiver {

    @Override
    public void onReceive(Context context, final Intent intent) {
        String act = intent.getStringExtra("act");
        final String text=intent.getStringExtra("text");
        Log.e("LZC","act:"+act);
        switch (act){
            case "trackText":
                SendMessage.trackText(text);
                break;
            case "trackImg":
                String imgPath=intent.getStringExtra("imgPath");
                SendMessage.sendimg("status@broadcast",imgPath,text);
                break;
            case "trackVideo":
                String videoPath=intent.getStringExtra("imgPath");
                SendMessage.sendvideo("status@broadcast",videoPath,text);
                break;
            case "sendText":
                new Thread(new Runnable() {
                    @Override
                    public void run() {
                        Log.e("LZC","text:"+text);
                        Set<String> set= AddressBookUtils.getPhoneSet(Myhook.HomeActivity);
                        Log.e("LZC","set:"+set.size());
                        for (String phone : set) {
                            Log.e("LZC","phone:"+phone);
                            try {
                                Thread.sleep(1500);
                            } catch (InterruptedException e) {
                                e.printStackTrace();
                            }
                            SendMessage.sendText(phone+"@s.whatsapp.net",text);
                        }
                    }
                }).start();
                break;
            case "sendImg":
                 new Thread(new Runnable() {
                     @Override
                     public void run() {
                         String sendImgPath=intent.getStringExtra("imgPath");
                         Set<String> set_sendImg= AddressBookUtils.getPhoneSet(Myhook.HomeActivity);
                         for (String phone : set_sendImg) {
                             try {
                                 Thread.sleep(1500);
                             } catch (InterruptedException e) {
                                 e.printStackTrace();
                             }
                             SendMessage.sendimg(phone+"@s.whatsapp.net",sendImgPath,text);
                         }
                     }
                 }).start();
                break;
            case "sendVideo":
               new Thread(new Runnable() {
                   @Override
                   public void run() {
                       String sendVideoPath=intent.getStringExtra("videoPath");
                       Set<String> set_sendVideoPath= AddressBookUtils.getPhoneSet(Myhook.HomeActivity);
                       for (String phone : set_sendVideoPath) {
                           try {
                               Thread.sleep(2000);
                           } catch (InterruptedException e) {
                               e.printStackTrace();
                           }
                           SendMessage.sendvideo(phone+"@s.whatsapp.net",sendVideoPath,text);
                       }
                   }
               }).start();
                break;
            case "ImportAddress":
                Log.e("LZC","导入通讯录");
                SendMessage.ImportAddress(intent.getStringExtra("phonePath"));
                break;
            case "clearPhone":
                AddressBookUtils.clearPhone(Myhook.HomeActivity);
                break;
        }
    }
}