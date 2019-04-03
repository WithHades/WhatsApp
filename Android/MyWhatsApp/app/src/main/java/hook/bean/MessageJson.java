package hook.bean;

/**
 * Created by DH on 2018/6/13.
 */

public class MessageJson {
    public int action;
    public int tasknum;
    public Context context;

    public int getAction() {
        return action;
    }

    public void setAction(int action) {
        this.action = action;
    }

    public int getTasknum() {
        return tasknum;
    }

    public void setTasknum(int tasknum) {
        this.tasknum = tasknum;
    }

    public Context getContext() {
        return context;
    }

    public void setContext(Context context) {
        this.context = context;
    }
}
