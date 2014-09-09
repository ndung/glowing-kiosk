/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package com.ics.ssk.ego.scheduler;

import org.apache.log4j.Logger;
import org.quartz.JobDetail;
import org.quartz.Scheduler;
import org.quartz.SchedulerException;
import org.quartz.SchedulerFactory;
import org.quartz.impl.StdSchedulerFactory;
import org.springframework.scheduling.quartz.CronTriggerBean;
import org.springframework.scheduling.quartz.JobDetailBean;

/**
 *
 * @author ICS Team
 */
public class SchedulerFactoryFactory {

    Scheduler sched;
    SchedulerFactory sf;
    Logger logger = Logger.getLogger(SchedulerFactoryFactory.class);

    public void start() throws SchedulerException {
        sf = new StdSchedulerFactory();
        sched = sf.getScheduler();
        sched.start();
    }

    public void stop() throws SchedulerException {
        sched.shutdown();
    }

    public boolean isRegistered(String jobName, String JobGroup) throws SchedulerException {
        JobDetail jd = sched.getJobDetail(jobName, JobGroup);
        if (jd != null) {
            return true;
        }
        return false;
    }

    public boolean isModified(String jobName, String JobGroup, CronTriggerBean ct) throws SchedulerException {
        CronTriggerBean t = (CronTriggerBean) sched.getTrigger(jobName, JobGroup);
        if (!t.getCronExpression().equalsIgnoreCase(ct.getCronExpression())) {
            return true;
        }
        return false;
    }

    public void setSchedule(JobDetailBean jobDetail, CronTriggerBean trigger) throws SchedulerException {
        sched.scheduleJob(jobDetail, trigger);
    }

    public void removeSchedule(String jobName, String jobGroup) throws SchedulerException {
        sched.deleteJob(jobName, jobGroup);
    }
}
