/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package com.ics.ssk.ego.manager;

import javax.servlet.http.HttpServletRequest;

public interface SessionManager {

    String setSession(HttpServletRequest request, Object object);

    String setSession(HttpServletRequest request, String key, Object object);

    Object getSession(HttpServletRequest request, String key);
}
