package hook.bean;

/**
 * Created by DH on 2018/6/20.
 */

public class AddressBookJson {
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

    public class Context{
        public int tasktype;
        public String filepath;
        public int nums;

        public int getTasktype() {
            return tasktype;
        }

        public void setTasktype(int tasktype) {
            this.tasktype = tasktype;
        }

        public String getFilepath() {
            return filepath;
        }

        public void setFilepath(String filepath) {
            this.filepath = filepath;
        }

        public int getNums() {
            return nums;
        }

        public void setNums(int nums) {
            this.nums = nums;
        }

    }

}
