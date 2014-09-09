package com.ics.ssk.ego.system;

import java.io.Serializable;
import java.util.Date;

public class UserSecurity implements Serializable {

    private static final long serialVersionUID = -4953594601104192537L;
    private Date dateLogin;

    public UserSecurity() {
        dateLogin = new Date();
    }

    public void setDateLogin(Date dateLogin) {
        this.dateLogin = dateLogin;
    }

    public Date getDateLogin() {
        return dateLogin;
    }
}
