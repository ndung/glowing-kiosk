/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package com.ics.ssk.ego.dao;

import com.ics.ssk.ego.model.Advertisement;
import java.util.List;

/**
 *
 * @author ndung
 */
public interface AdvertisementDao {
    List<Advertisement> getHeadline();
    List<Advertisement> getReceipt();
}
