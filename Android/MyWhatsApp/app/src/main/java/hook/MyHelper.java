package hook;


import android.graphics.Bitmap;
import android.media.MediaMetadataRetriever;
import android.media.ThumbnailUtils;
import android.provider.MediaStore;
import android.util.Base64;

import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.FileReader;
import java.io.FileWriter;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;
import java.io.UnsupportedEncodingException;
import java.math.BigInteger;
import java.nio.ByteBuffer;
import java.nio.channels.FileChannel;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;
import java.util.ArrayList;
import java.util.List;
import java.util.Random;
import java.util.UUID;

public class MyHelper {

    public static List list;
    private static String PATH = "/sdcard/";
    public static String userid = null;
    private static String SDDirPath = "/sdcard/";


    //region 文件操作

    //创建侠客目录
    public static void createFile() {
        File file = new File(PATH);
        if (!file.exists()) {
            file.mkdirs();
        }
    }


    //在SD卡上创建文件
    public static File createSDFile(String fileName) throws IOException {
        File file = new File(PATH + fileName);
        if (!file.exists()) {
            try {
                file.createNewFile();
            } catch (Exception ex) {

            }
        }
        return file;
    }

    public static void appendSDDirString(String key, String value) {
        FileWriter fw = null;
        try {
            createFile();
            File file = new File(SDDirPath + key);
            fw = new FileWriter(file, true);//以追加的模式将字符写入
            BufferedWriter bw = new BufferedWriter(fw);//包裹一层缓冲流 增强IO功能
            bw.write(value);
            bw.write("\r\n");
            bw.flush();//将内容一次性写入文件
            bw.close();
        } catch (Exception e) {
            e.printStackTrace();
        } finally {
            if (fw != null) {
                try {
                    fw.close();
                } catch (IOException e) {
                    e.printStackTrace();
                }
            }
        }
    }
    

    //删除SD卡上的文件
    public static boolean deleteSDFile(String fileName) {
        File file = new File(PATH + fileName);
        if (file == null || !file.exists() || file.isDirectory())
            return false;
        return file.delete();
    }

    //删除SD卡
    public static boolean deleteSDDirFile(String key) {
        File file = new File(SDDirPath + key);
        if (!file.exists() || file.isDirectory())
            return false;
        return file.delete();
    }

    public static String getFilePath() {
        return PATH;
    }

    public static boolean isfileExist(String fileName) {
        File file = new File(PATH + fileName);
        if (file.exists()) {
            return true;
        } else {
            return false;
        }
    }

    //删除SD卡上的文件
    public static boolean deleteFile(String key) {
        File file = new File(PATH + key);
        if (!file.exists() || file.isDirectory())
            return false;
        return file.delete();
    }

    public static List<String> readToList(String key) {
        List<String> list = new ArrayList<>();
        File file = new File(PATH, key);
        try {
            FileReader fr = new FileReader(file);
            BufferedReader br = new BufferedReader(fr);
            String str = "";
            while ((str = br.readLine()) != null) {
                list.add(str);
            }
            br.close();
            fr.close();
        } catch (FileNotFoundException e) {
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        }
        return list;
    }

    public static List<String> readToList_file(String filepath) {
        List<String> list = new ArrayList<>();
        File file = new File(filepath);
        try {
            FileReader fr = new FileReader(file);
            BufferedReader br = new BufferedReader(fr);
            String str = "";
            while ((str = br.readLine()) != null) {
                list.add(str);
            }
            br.close();
            fr.close();
        } catch (FileNotFoundException e) {
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        }
        return list;
    }

    public static List<String> readToListByPath(String path) {
        List<String> list = new ArrayList<String>();
        File file = new File(path);

        try {
            FileReader fr = new FileReader(file);
            BufferedReader br = new BufferedReader(fr);
            String str = "";
            while ((str = br.readLine()) != null) {
                String content = str;
                list.add(content);
            }
            br.close();
            fr.close();
        } catch (FileNotFoundException e) {
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        }
        return list;
    }

    public static String GetString(String key, String DefaultString) {
        try {
            String encoding = "UTF-8";
            File file = new File(PATH + key);
            if (file.isFile() && file.exists()) { //判断文件是否存在
                InputStreamReader read = new InputStreamReader(
                        new FileInputStream(file), encoding);//考虑到编码格式
                BufferedReader bufferedReader = new BufferedReader(read);
                String lineTxt = bufferedReader.readLine();
                read.close();
                return lineTxt == null ? DefaultString : lineTxt;
            } else {
                return DefaultString;
            }
        } catch (Exception e) {
            e.printStackTrace();
            return DefaultString;
        }
    }

    public static void appendString(String key, String value) {
        FileWriter fw = null;
        try {
            createFile();
            File file = new File(PATH + key);
            fw = new FileWriter(file, true);//以追加的模式将字符写入
            BufferedWriter bw = new BufferedWriter(fw);//包裹一层缓冲流 增强IO功能
            bw.write(value);
            bw.write("\r\n");
            bw.flush();//将内容一次性写入文件
            bw.close();
        } catch (Exception e) {
            e.printStackTrace();
        } finally {
            if (fw != null) {
                try {
                    fw.close();
                } catch (IOException e) {
                    e.printStackTrace();
                }
            }
        }
    }


    public static void SetString(String key, String value) {
        SetString(PATH, key, value);
    }

    public static void SetString(String path, String key, String value) {
        BufferedWriter fw = null;
        try {
            createFile();
            //对定位修改进行判断是修改定位还是站街模式的定位
            key = gpsString(key);
            File file = new File(path + key);
            fw = new BufferedWriter(new OutputStreamWriter(new FileOutputStream(file, false), "UTF-8")); // 指定编码格式，以免读取时中文字符异常
            fw.write(value);
            fw.flush();
        } catch (Exception e) {
            e.printStackTrace();
        } finally {
            if (fw != null) {
                try {
                    fw.close();
                } catch (IOException e) {
                    e.printStackTrace();
                }
            }
        }
    }

    private static String gpsString(String key) {
        //gps 定位  gps-move 站街定位(一直移动)
        String gps = "gps";
        String gpsMove = "gps-move";
        if (key.equals(gps) || key.equals(gpsMove)) {
            if (key.equals(gps)) {
            }
            return "gps";
        }
        return key;
    }

    public static byte[] ReadFile(String filename) {
        byte[] data = new byte[0];
        File f = new File(filename);
        if (!f.exists()) {
            return data;
        }
        try {
            FileChannel channel = null;
            FileInputStream fs = null;
            fs = new FileInputStream(f);
            channel = fs.getChannel();
            ByteBuffer byteBuffer = ByteBuffer.allocate((int) channel.size());
            data = byteBuffer.array();
        } catch (Exception e) {
            e.printStackTrace();
        }
        return data;
    }

    /**
     * 复制单个文件
     *
     * @param oldPath String 原文件路径 如：c:/fqf.txt
     * @param newPath String 复制后路径 如：f:/fqf.txt
     * @return boolean
     */
    public static void copyFile(String oldPath, String newPath) {
        try {
            int bytesum = 0;
            int byteread = 0;
            File oldfile = new File(oldPath);
            if (oldfile.exists()) { //文件存在时
                InputStream inStream = new FileInputStream(oldPath); //读入原文件
                FileOutputStream fs = new FileOutputStream(newPath);
                byte[] buffer = new byte[1444];
                int length;
                while ((byteread = inStream.read(buffer)) != -1) {
                    bytesum += byteread; //字节数 文件大小
                    System.out.println(bytesum);
                    fs.write(buffer, 0, byteread);
                }
                inStream.close();
            }
        } catch (Exception e) {
            System.out.println("复制单个文件操作出错");
            e.printStackTrace();
        }

    }

    //endregion

    //region MD5加密
    public static String md5Stream(InputStream is) throws IOException {
        String md5 = "";
        try {
            byte[] bytes = new byte[4096];
            int read = 0;
            MessageDigest digest = MessageDigest.getInstance("MD5");
            while ((read = is.read(bytes)) != -1) {
                digest.update(bytes, 0, read);
            }
            md5 = new BigInteger(1, digest.digest()).toString(16);
            while (md5.length() < 32) {
                md5 = "0" + md5;
            }
            return md5;
        } catch (Exception e) {
            e.printStackTrace();
        }
        return md5;
    }


    public static String MD5(String inStr) {
        MessageDigest md5 = null;
        try {
            md5 = MessageDigest.getInstance("MD5");
        } catch (Exception e) {
            System.out.println(e.toString());
            e.printStackTrace();
            return "";
        }
        char[] charArray = inStr.toCharArray();
        byte[] byteArray = new byte[charArray.length];

        for (int i = 0; i < charArray.length; i++)
            byteArray[i] = (byte) charArray[i];

        byte[] md5Bytes = md5.digest(byteArray);

        StringBuilder hexValue = new StringBuilder();

        for (int i = 0; i < md5Bytes.length; i++) {
            int val = ((int) md5Bytes[i]) & 0xff;
            if (val < 16)
                hexValue.append("0");
            hexValue.append(Integer.toHexString(val));
        }

        return hexValue.toString();
    }

    //endregion

    //region hook处理

    /**
     * @param urlPath wx图片url转路径
     * @return 返回文件名
     */
    public static String getFileName(String urlPath) {
        try {
            // Create MD5 Hash
            MessageDigest digest = MessageDigest.getInstance("MD5");
            digest.update(urlPath.getBytes());
            byte messageDigest[] = digest.digest();

            // Create Hex String
            StringBuilder hexString = new StringBuilder();
            for (int i = 0; i < messageDigest.length; i++)
                hexString.append(Integer.toHexString(0xFF & messageDigest[i]));
            return hexString.toString();

        } catch (NoSuchAlgorithmException e) {
            e.printStackTrace();
        }
        Random random = new Random();
        StringBuilder sb = new StringBuilder("abc");
        sb.append(String.valueOf(random.nextInt(100)));
        sb.append(String.valueOf(random.nextInt(100)));
        sb.append(String.valueOf(random.nextInt(100)));
        sb.append(String.valueOf(random.nextInt(100)));
        return sb.toString();
    }

    public static Bitmap retrieveVideoFrameFromVideo(String filePath) {
        Bitmap bitmap = null;
        MediaMetadataRetriever metadataRetriever = null;
        try {
            metadataRetriever = new MediaMetadataRetriever();
            metadataRetriever.setDataSource(filePath);
            bitmap = metadataRetriever.getFrameAtTime();
        } catch (Exception e) {
            e.printStackTrace();
        } finally {
            if (metadataRetriever != null) {
                metadataRetriever.release();
            }
        }
        if (bitmap == null) {
            try {
                bitmap = ThumbnailUtils.createVideoThumbnail(filePath, MediaStore.Video.Thumbnails.MICRO_KIND);
            } catch (Exception e) {
                e.printStackTrace();
            }
        }
        return bitmap;
    }

    /**
     * 发送消息
     *
     * @param key
     * @param value
     */
    public static void SendMsgToServer(final String key, final String value) {
        new Thread() {
            @Override
            public void run() {

                File file = new File("/sdcard/share_" + UUID.randomUUID());

                BufferedWriter fw = null;
                try {
                    fw = new BufferedWriter(new OutputStreamWriter(new FileOutputStream(file, false), "UTF-8"));
                    fw.write(key + "\n" + StringToBase64(value));
                    fw.flush();
                } catch (Exception e) {
                    e.printStackTrace();
                } finally {
                    if (fw != null) {
                        try {
                            fw.close();
                        } catch (IOException e) {
                            e.printStackTrace();
                        }
                    }
                }

            }
        }.start();
    }

    //endregion

    //region base64转换

    /**
     * 字符串转base64
     *
     * @param str
     * @return
     * @throws UnsupportedEncodingException
     */
    public static String StringToBase64(String str) throws UnsupportedEncodingException {
        String strBase64 = new String(Base64.encode(str.getBytes("UTF-8"), Base64.DEFAULT));
        return strBase64.replace("\r", "").replace("\n", "");
    }

    /**
     * base转字符串
     *
     * @param strBase64
     * @return
     * @throws UnsupportedEncodingException
     */
    public static String Base64ToString(String strBase64) throws UnsupportedEncodingException {
        return new String(Base64.decode(strBase64.getBytes(), Base64.DEFAULT), "UTF-8");
    }

    //endregion
    
}
