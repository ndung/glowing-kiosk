package com.ics.ssk.ego.web;

import net.sourceforge.stripes.action.After;
import net.sourceforge.stripes.action.Before;
import net.sourceforge.stripes.action.DefaultHandler;
import net.sourceforge.stripes.action.DontValidate;
import net.sourceforge.stripes.action.ForwardResolution;
import net.sourceforge.stripes.action.RedirectResolution;
import net.sourceforge.stripes.action.Resolution;
import net.sourceforge.stripes.action.UrlBinding;

@UrlBinding("/egoblockscreen.html")
public class EgoBlockScreen extends BaseActionBean {

    private String VIEW1 = "/pages/ego/blockscreen.jsp";    
    
    @Before
    @DontValidate
    public void startup() {        
    }

    @After
    @DontValidate
    public void endup() {
    }

    @DontValidate
    public Resolution back() {
        return new RedirectResolution(EgoWelcome.class);
    }

    @DontValidate
    @DefaultHandler
    public Resolution view() {        
        return new ForwardResolution(VIEW1);
    }
    
    
}
