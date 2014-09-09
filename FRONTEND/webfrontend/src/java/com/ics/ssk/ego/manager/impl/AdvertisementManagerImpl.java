/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package com.ics.ssk.ego.manager.impl;

import com.ics.ssk.ego.dao.AdvertisementDao;
import com.ics.ssk.ego.manager.AdvertisementManager;
import com.ics.ssk.ego.model.Advertisement;
import java.util.List;

/**
 *
 * @author ndung
 */
public class AdvertisementManagerImpl implements AdvertisementManager {

    private AdvertisementDao advertisementDao;

    public void setAdvertisementDao(AdvertisementDao advertisementDao) {
        this.advertisementDao = advertisementDao;
    }

    @Override
    public List<Advertisement> getHeadline() {
        return advertisementDao.getHeadline();
    }

    @Override
    public List<Advertisement> getReceipt() {
        return advertisementDao.getReceipt();
    }

}