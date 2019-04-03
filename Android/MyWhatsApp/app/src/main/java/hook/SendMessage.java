package hook;

import android.content.ContentResolver;
import android.content.ContentUris;
import android.content.ContentValues;
import android.content.Context;
import android.database.Cursor;
import android.net.Uri;
import android.provider.ContactsContract;
import android.provider.MediaStore;
import android.text.TextUtils;
import android.util.Log;

import java.io.File;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

import de.robv.android.xposed.XposedHelpers;
import hook.util.AddressBookUtils;

/**
 * Created by DH on 2018/6/7.
 */

public class SendMessage {

    private static int textTrack_backgroundColor = 0;

    /**
     * 发送文字
     *
     * @param idphone 用户ID
     * @param text    内容
     */
    public static void sendText(String idphone, String text) {
        try {
            Class apn = Myhook.lpp.classLoader.loadClass("com.whatsapp.apn");
            Object newApn = XposedHelpers.callStaticMethod(apn, "a");
            List ids = Arrays.asList(new String[]{idphone});
            String text_message = text;
            List list = Arrays.asList();
            XposedHelpers.callMethod(newApn, "a", ids, text_message, null, null, list, false, false);
            Log.e("LZC", "发送文字：" + "成功了");
        } catch (ClassNotFoundException e) {
            e.printStackTrace();
        }
    }

    /**
     * 发送文字动态
     *
     * @param text
     */
    public static void trackText(String text) {
        try {

            if (text.isEmpty()) {
                text = null;
            }
            Class apn = Myhook.lpp.classLoader.loadClass("com.whatsapp.apn");
            Object newApn = XposedHelpers.callStaticMethod(apn, "a");

            Object f = XposedHelpers.getObjectField(newApn, "b");
            Object long_time = XposedHelpers.callMethod(f, "c");

            Object k = XposedHelpers.getObjectField(newApn, "l");
            Log.e("LZC", "发送朋友圈k：" + k);
            Object j = XposedHelpers.callMethod(k, "a", "status@broadcast", text, long_time, null, null);
            Log.e("LZC", "发送朋友圈j：" + j);

            XposedHelpers.callMethod(newApn, "d", j);
            Log.e("LZC", "发送朋友圈：" + "调用d方法");

            Class TextData = Myhook.lpp.classLoader.loadClass("com.whatsapp.TextData");

            Object newtextData = XposedHelpers.newInstance(TextData);

            Log.e("LZC", "发送朋友圈newtextData：" + newtextData);

            textTrack_backgroundColor = backgroundColor(textTrack_backgroundColor);
            Log.e("LZC", "发送朋友圈textTrack_backgroundColor：" + textTrack_backgroundColor);

            XposedHelpers.setObjectField(newtextData, "backgroundColor", textTrack_backgroundColor);
            XposedHelpers.setObjectField(newtextData, "textColor", -1);
            XposedHelpers.setObjectField(newtextData, "fontStyle", 0);
            XposedHelpers.setObjectField(j, "I", newtextData);

            XposedHelpers.callMethod(XposedHelpers.getObjectField(newApn, "n"), "a", j);
            XposedHelpers.callMethod(XposedHelpers.getObjectField(newApn, "A"), "a", j, 0, 1);

            Log.e("LZC", "发送朋友圈" + "成功了！");

        } catch (ClassNotFoundException e) {
            e.printStackTrace();
        }
    }

    /**
     * 设置动态的颜色
     *
     * @param historColor 默认值
     * @return
     */
    public static int backgroundColor(int historColor) {
        try {
            Class statusplayback = Myhook.lpp.classLoader.loadClass("com.whatsapp.statusplayback.x");
            int[] iArr = (int[]) XposedHelpers.getStaticObjectField(statusplayback, "b");
            int number = (int) XposedHelpers.callStaticMethod(statusplayback, "a", iArr, historColor);
            int color = iArr[(number + 1) % iArr.length];
            return color;
        } catch (ClassNotFoundException e) {
            e.printStackTrace();
        }
        return 0;
    }

    /**
     * 发送图片
     *
     * @param idphone 用户ID
     * @param imgpath 图片路劲
     * @param text    文字描述
     */
    public static void sendimg(String idphone, String imgpath, String text) {
        try {
            if (text.isEmpty()) {
                text = null;
            }
            Class aiz = Myhook.lpp.classLoader.loadClass("com.whatsapp.aiz");
            Log.e("LZC", "发送图片aiz:" + aiz);
            Object newaiz = XposedHelpers.callStaticMethod(aiz, "a");
            Log.e("LZC", "发送图片newaiz:" + newaiz);
            Uri uri = Uri.fromFile(new File(imgpath));
//            Uri uri=getUri(imgpath);
            Log.e("LZC", "发送图片uri:" + uri);
            List id = Arrays.asList(new String[]{idphone});

            Class media_b = Myhook.lpp.classLoader.loadClass("com.whatsapp.media.b");
            Object newmedia_b = XposedHelpers.callStaticMethod(media_b, "a");
            byte by = 1;
            String str = uri.toString();
            Log.e("LZC", "发送图片str:" + str);
            XposedHelpers.callMethod(newaiz, "a", id, uri, XposedHelpers.callMethod(newmedia_b, "a", str, by, false), 0, null, null, text, false, false, null);
            Log.e("LZC", "发送图片" + "成功了！");
        } catch (ClassNotFoundException e) {
            e.printStackTrace();
        }
    }

    /**
     * path转uri
     */
    private static Uri getUri(String path) {
        Uri uri = null;
        if (path != null) {
            path = Uri.decode(path);
            Log.d("LZC", "path2 is " + path);
            ContentResolver cr = Myhook.HomeActivity.getContentResolver();
            StringBuffer buff = new StringBuffer();
            buff.append("(")
                    .append(MediaStore.Images.ImageColumns.DATA)
                    .append("=")
                    .append("'" + path + "'")
                    .append(")");
            Cursor cur = cr.query(
                    MediaStore.Images.Media.EXTERNAL_CONTENT_URI,
                    new String[]{MediaStore.Images.ImageColumns._ID},
                    buff.toString(), null, null);
            int index = 0;
            for (cur.moveToFirst(); !cur.isAfterLast(); cur
                    .moveToNext()) {
                index = cur.getColumnIndex(MediaStore.Images.ImageColumns._ID);
// set _id value
                index = cur.getInt(index);
            }
            if (index == 0) {
//do nothing
            } else {
                Uri uri_temp = Uri.parse("content://media/external/images/media/" + index);
                Log.d("LZC", "uri_temp is " + uri_temp);
                if (uri_temp != null) {
                    uri = uri_temp;
                }
            }
        }
        return uri;
    }


    /**
     * 发送视频
     *
     * @param idphone   用户id  如传入 status@broadcast  则是发送视频动态
     * @param videoPath 视频路劲
     * @param text      文字描述
     */
    public static void sendvideo(String idphone, String videoPath, String text) {
        try {
            if (text.isEmpty()) {
                text = null;
            }
            Class MediaData = Myhook.lpp.classLoader.loadClass("com.whatsapp.MediaData");
            Log.e("LZC", "MediaData:" + MediaData);
            Object nweData = XposedHelpers.newInstance(MediaData);
//            File videofile=new File("/storage/emulated/0/video.mp4");
            File videofile = new File(videoPath);
            Log.e("LZC", "videofile:" + videofile);
            if (videofile.exists()) {
                XposedHelpers.setObjectField(nweData, "file", videofile);
                XposedHelpers.setObjectField(nweData, "trimFrom", 0l);
                XposedHelpers.setObjectField(nweData, "trimTo", 0l);
            }
            Log.e("LZC", "file:" + XposedHelpers.getObjectField(nweData, "file"));
            Log.e("LZC", "trimFrom:" + XposedHelpers.getObjectField(nweData, "trimFrom"));
            Log.e("LZC", "trimTo:" + XposedHelpers.getObjectField(nweData, "trimTo"));
            Class wu = Myhook.lpp.classLoader.loadClass("com.whatsapp.wu");
            Object newwu = XposedHelpers.callStaticMethod(wu, "a");
            byte by = 3;
            Object aih = XposedHelpers.callMethod(newwu, "a", Arrays.asList(new String[]{idphone}), nweData, by, 0, text, null, null, null, false, false, null);
            Log.e("LZC", "aih:" + aih);
            Class apn = Myhook.lpp.classLoader.loadClass("com.whatsapp.apn");
            Object newapn = XposedHelpers.callStaticMethod(apn, "a");
            Class MediaFileUtils = Myhook.lpp.classLoader.loadClass("com.whatsapp.util.MediaFileUtils");
            Object byt = XposedHelpers.callStaticMethod(MediaFileUtils, "a", videofile.getAbsolutePath(), 0l);
            Log.e("LZC", "byt:" + byt);
            XposedHelpers.callMethod(newapn, "a", aih, byt);
            Log.e("LZC", "发送视频成功:");
        } catch (ClassNotFoundException e) {
            e.printStackTrace();
        }
    }

    /**
     * 导入通讯录
     */
    public static void ImportAddress(String phonePath) {
        List<String> list_phone = MyHelper.readToList_file(phonePath);
        for (int i = 0; i < list_phone.size(); i++) {
            String name = AddressBookUtils.getName();
            Log.e("LZC","name:"+name);
            boolean boo= AddressBookUtils.saveContact(Myhook.HomeActivity, list_phone.get(i), name);
        }
    }
}
