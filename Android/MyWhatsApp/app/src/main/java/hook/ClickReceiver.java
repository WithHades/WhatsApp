package hook;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.util.Log;

/**
 * Created by Administrator on 2018/7/13.
 */

public class ClickReceiver extends BroadcastReceiver {
    @Override
    public void onReceive(Context context, Intent intent) {
        String act = intent.getStringExtra("act");
        switch (act){
            case "AgreeAndContinue"://同意并继续
                Log.e("LZC","开始同意"+Myhook.EULA_button);
                try {
                    Myhook.EULA_button.performClick();
                } catch (Exception e) {
                    Log.e("LZC","点击同意并继续异常"+e.getMessage());
                }
                Log.e("LZC","点击同意并继续");
                break;
        }
    }
}
