package hook.util;

import java.io.BufferedOutputStream;
import java.io.IOException;
import java.net.Socket;

/**
 * Created by DH on 2018/6/24.
 */

public class WriteUtil {

    /**
     * 写数据
     * @param socket
     * @param s
     * @throws IOException
     */
    public static void write(Socket socket, String s) throws IOException {
        BufferedOutputStream outputStream = new BufferedOutputStream(socket.getOutputStream());
        byte bytes[] = s.getBytes();//String转换为byte[]
//        String LengByte = "//////////";
//        byte[] LengBytes = LengByte.getBytes();
//        String length = bytes.length + "";//"158602"
//        byte[] bytes1 = length.getBytes();//String转换为byte[]{1,5,8,6,0,2} {1, 5, 8, 6, 0, 2, 0, 0};
//        if (bytes1.length <= 8) {
//            for (int i = 0; i < bytes1.length; i++) {
//                LengBytes[i + 1] = bytes1[i];
//            }
//        }
//        byte[] bytes2 = unitByteArray(LengBytes, bytes);
        outputStream.write(bytes);
        outputStream.flush();
    }

    /**
     * 合并byte数组
     */
    public static byte[] unitByteArray(byte[] byte1, byte[] byte2) {
        byte[] unitByte = new byte[byte1.length + byte2.length];
        System.arraycopy(byte1, 0, unitByte, 0, byte1.length);
        System.arraycopy(byte2, 0, unitByte, byte1.length, byte2.length);
        return unitByte;
    }
}
