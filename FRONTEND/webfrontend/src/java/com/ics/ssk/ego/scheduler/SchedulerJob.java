/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package com.ics.ssk.ego.scheduler;

import java.text.ParseException;
import java.util.HashMap;
import java.util.Map;
import org.apache.log4j.Logger;
import org.quartz.SchedulerException;
import org.springframework.scheduling.quartz.CronTriggerBean;
import org.springframework.scheduling.quartz.JobDetailBean;

/**
 *
 * @author ICS Team
 */
public class SchedulerJob {

    Logger logger = Logger.getLogger(SchedulerJob.class);
    
    private SchedulerFactoryFactory schedulerFactoryFactory;

    public void createJob(String jobName, String jobGroup, String jobClassName, String cronExp) throws SchedulerException, ClassNotFoundException, ParseException {
        JobDetailBean job = new JobDetailBean();
        job.setName(jobName);
        job.setGroup(jobGroup);
        CronTriggerBean trigger = new CronTriggerBean();
        trigger.setName(jobName);
        trigger.setGroup(jobGroup);

        job.setJobClass(Class.forName(jobClassName));
        Map map = new HashMap();
        map.put("", null);
        job.setJobDataAsMap(map);
        trigger.setJobDetail(job);
        trigger.setCronExpression(cronExp);

        if (!schedulerFactoryFactory.isRegistered(jobName, jobGroup)) {
            logger.debug("this job is unregistered, register this job...");
            schedulerFactoryFactory.setSchedule(job, trigger);
        } else {
            if (schedulerFactoryFactory.isModified(jobName, jobGroup, trigger)) {
                logger.debug("this job is modified, register this job...");
                schedulerFactoryFactory.removeSchedule(jobName, jobGroup);
                schedulerFactoryFactory.setSchedule(job, trigger);
            }
        }
    }

    public void deleteJob(String jobName, String jobGroup, String jobClassName, String cronExp) throws SchedulerException{
        schedulerFactoryFactory.removeSchedule(jobName, jobGroup);
    }

    /**
     * @param schedulerFactoryFactory the schedulerFactoryFactory to set
     */
    public void setSchedulerFactoryFactory(SchedulerFactoryFactory schedulerFactoryFactory) {
        this.schedulerFactoryFactory = schedulerFactoryFactory;
    }

    
}
