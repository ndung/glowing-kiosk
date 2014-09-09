/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package com.ics.ssk.ego.manager.impl;

import com.ics.ssk.ego.manager.SessionManager;
import java.util.concurrent.atomic.AtomicInteger;
import javax.servlet.http.HttpServletRequest;

public class SessionManagerImpl implements SessionManager {

    private AtomicInteger counter = new AtomicInteger(0);

    @Override
    public String setSession(HttpServletRequest request, Object object) {
        return setSession(request, null, object);
    }

    @Override
    public Object getSession(HttpServletRequest request, String key) {
        Object object = request.getSession().getAttribute(key);
        return object;
    }

    @Override
    public String setSession(HttpServletRequest request, String id, Object object) {
        if (id == null) {
            int key = counter.incrementAndGet();
            if (key > 1000000) {
                counter = new AtomicInteger(0);
                key = counter.incrementAndGet();
            }
            id = String.valueOf(key);
        }
        request.getSession().setAttribute(id, object);
        request.getSession().setMaxInactiveInterval(10 * 60);
        return id;
    }
}
