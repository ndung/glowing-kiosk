package com.ics.ssk.ego.system;

import com.ics.ssk.ego.util.ConstantaUtil;
import java.io.IOException;
import javax.servlet.Filter;
import javax.servlet.FilterChain;
import javax.servlet.FilterConfig;
import javax.servlet.ServletException;
import javax.servlet.ServletRequest;
import javax.servlet.ServletResponse;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

//import net.sourceforge.stripes.util.StringUtil;
public class SecurityFilter extends ConstantaUtil implements Filter {

    @Override
    public void doFilter(ServletRequest servletRequest, ServletResponse servletResponse, FilterChain filterChain) throws IOException, ServletException {

        HttpServletRequest request = (HttpServletRequest) servletRequest;
        HttpServletResponse response = (HttpServletResponse) servletResponse;

        UserSecurity user = null;
        boolean access = true;
        String url = request.getServletPath().substring(1).toLowerCase();
        try {
            user = (UserSecurity) request.getSession().getAttribute(USER_SESSION);
        } catch (Exception e) {
            user = null;
        }

        if (url.endsWith(".jsp") || url.endsWith(".html")) {
            access = false;
        }        
        if (!access && (url.startsWith("ego"))) {
            access = true;
        }
        if (!access && user != null) {
            access = true;
        }
//        
        if (access) {
            filterChain.doFilter(request, response);
        } else {
            response.sendRedirect(request.getContextPath() + "/ego.html");
        }
    }

    @Override
    public void init(FilterConfig arg0) throws ServletException {
    }

    @Override
    public void destroy() {
    }
}
