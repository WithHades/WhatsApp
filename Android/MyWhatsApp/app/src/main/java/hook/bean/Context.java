package hook.bean;

import java.util.List;

/**
 * Created by DH on 2018/6/13.
 */

public class Context {
    public int tasktype;
    public String txtmsg;
    public List list;

    public int getTasktype() {
        return tasktype;
    }

    public void setTasktype(int tasktype) {
        this.tasktype = tasktype;
    }

    public String getTxtmsg() {
        return txtmsg;
    }

    public void setTxtmsg(String txtmsg) {
        this.txtmsg = txtmsg;
    }

    public List getList() {
        return list;
    }

    public void setList(List list) {
        this.list = list;
    }
}
