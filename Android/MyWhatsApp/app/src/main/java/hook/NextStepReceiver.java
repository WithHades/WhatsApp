package hook;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.util.Log;

/**
 * Created by Administrator on 2018/7/13.
 */

public class NextStepReceiver  extends BroadcastReceiver{
    @Override
    public void onReceive(Context context, Intent intent) {
        String act = intent.getStringExtra("act");
        switch (act){
            case "NextStep":
                Log.e("LZC","开始下一步！");
                Myhook.registerPhone_button.performClick();
                Log.e("LZC","点击下一步！");
                break;
        }
    }
}
