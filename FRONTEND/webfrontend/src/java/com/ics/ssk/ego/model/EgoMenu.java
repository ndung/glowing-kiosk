package com.ics.ssk.ego.model;

import com.ics.ssk.ego.util.StringUtil;

public class EgoMenu implements java.io.Serializable, Comparable<EgoMenu> {

    public static String GROUP = "group";
    public static String ID = "id";
    private static final long serialVersionUID = -3854620688261625968L;
    private Integer id;
    private String title;
    private String link;
    private String group;

    public EgoMenu() {
    }

    public Integer getId() {
        return this.id;
    }

    public void setId(Integer id) {
        this.id = id;
    }

    public String getTitle() {
        return this.title;
    }

    public void setTitle(String title) {
        this.title = title;
    }

    public String getLink() {
        return this.link;
    }

    public void setLink(String link) {
        this.link = link;
    }

    @Override
    public int compareTo(EgoMenu o) {
        String a = StringUtil.addLeadingZeroes(this.getId(), 6);
        String b = StringUtil.addLeadingZeroes(o.getId(), 6);
        return a.compareTo(b);
    }

    public String getGroup() {
        return group;
    }

    public void setGroup(String group) {
        this.group = group;
    }
}
