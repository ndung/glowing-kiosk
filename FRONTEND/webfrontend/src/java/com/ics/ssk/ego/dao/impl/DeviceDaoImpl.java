/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package com.ics.ssk.ego.dao.impl;

import com.ics.ssk.ego.dao.DeviceDao;
import com.ics.ssk.ego.model.CardDispenser;
import com.ics.ssk.ego.model.Device;
import com.ics.ssk.ego.model.SmartPayout;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Types;
import java.util.ArrayList;
import java.util.List;
import org.springframework.dao.DataAccessException;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.jdbc.core.ResultSetExtractor;
import org.springframework.jdbc.core.RowMapper;


/**
 *
 * @author ndung
 */
public class DeviceDaoImpl implements DeviceDao {

    String selectSql = "select idx, device_code, denom, max_payout_note, current_payout_note, current_cashbox_note, current_routing, description "
            + " from ssp_inventory ";

    private JdbcTemplate jdbcTemplate;

    public void setJdbcTemplate(JdbcTemplate jdbcTemplate) {
        this.jdbcTemplate = jdbcTemplate;
    }
        
    @Override
    public void updateSmartPayoutInventory(String deviceCode, int denom, int currentPayoutNote, String spFlag) {
        if (spFlag.equals("0")) {
            String sql = "update ssp_inventory set current_payout_note = (current_payout_note + ?) where device_code = ? and denom = ?";
            Object[] param = new Object[]{currentPayoutNote, deviceCode, denom};
            jdbcTemplate.update(sql, param);            
        } else {
            updateCashboxInventory(deviceCode, denom, currentPayoutNote);
        }
    }

    @Override
    public void updateCashboxInventory(String deviceCode, int denom, int currentCashNote) {
        String sql = "update ssp_inventory set current_cashbox_note = (current_cashbox_note + ?) where device_code = ? and denom = ?";
        Object[] param = new Object[]{currentCashNote, deviceCode, denom};
        jdbcTemplate.update(sql, param);        
    }

    @Override
    public void insertSmartPayoutHistory(String deviceCode, int denom, String command, int payout, int cashbox, String stan) {
        String sql = "insert into ssp_history (device_code, denom, command, payout, cashbox, stan) values (?,?,?,?,?,?)";
        Object[] param = new Object[]{deviceCode, denom, command, payout, cashbox, stan};
        jdbcTemplate.update(sql, param);
    }

    @Override
    public void insertSmartPayoutHistory(String deviceCode, int denom, String command, int payout, int cashbox) {
        String sql = "insert into ssp_history (device_code, denom, command, payout, cashbox) values (?,?,?,?,?)";
        Object[] param = new Object[]{deviceCode, denom, command, payout, cashbox};
        jdbcTemplate.update(sql, param);
    }

    @Override
    public void updateSmartPayoutHistory(String stan, String command, String deviceCode, int denom) {
        String sql = "update ssp_history set stan = ? where command = ? and device_code = ? and denom = ? order by id desc limit 1 ";
        Object[] param = new Object[]{stan, command, deviceCode, denom};
        jdbcTemplate.update(sql, param);
    }

    @Override
    public void updateCardDispenserInventory(String deviceCode, int cardAmount) {
        String sql = "update card_dispenser_inventory set current_amount = (current_amount - ?) where device_code = ?";
        Object[] param = new Object[]{cardAmount, deviceCode};
        jdbcTemplate.update(sql, param);
    }

    @Override
    public List<SmartPayout> getSmartPayoutInventory() {
        String sql = selectSql;
        return jdbcTemplate.query(sql, new SmartPayoutRowMapper());
    }

    public List<SmartPayout> getSmartPayoutInventory(String deviceCode) {
        String sql = selectSql + "where device_code = ?";
        Object[] param = new Object[]{deviceCode};
        return jdbcTemplate.query(sql, param, new SmartPayoutRowMapper());
    }

    @Override
    public SmartPayout getSmartPayoutInventory(String deviceCode, int denom) {
        String sql = selectSql + "where device_code = ? and denom = ?";
        Object[] param = new Object[]{deviceCode, denom};
        List<SmartPayout> l = jdbcTemplate.query(sql, param, new SmartPayoutRowMapper());
        return l.get(0);
    }

    @Override
    public List<SmartPayout> getNonZeroSmartPayoutInventory(String deviceCode) {
        String sql = selectSql + "where device_code = ? and current_payout_note > ? order by denom desc";
        Object[] param = new Object[]{deviceCode, 0};
        return jdbcTemplate.query(sql, param, new SmartPayoutRowMapper());
    }

    @Override
    public void emptySmartPayout(String deviceCode) {
        for (SmartPayout sp : getSmartPayoutInventory()) {
            if (sp.getDeviceCode().equalsIgnoreCase(deviceCode)) {
                String sql = "update ssp_inventory set current_payout_note = ?, current_cashbox_note = (current_cashbox_note + ?)"
                        + " where device_code = ? and denom = ?";
                Object[] param = new Object[]{0, sp.getCurrentPayoutNote(), deviceCode, sp.getDenom()};
                jdbcTemplate.update(sql, param);
                sql = "insert into ssp_history (device_code, denom, stan, command, payout, cashbox) values (?,?,?,?,?,?)";
                param = new Object[]{deviceCode, sp.getDenom(), null, "empty", 0, sp.getCurrentPayoutNote()};
                jdbcTemplate.update(sql, param);
            }
        }
    }

    @Override
    public void clearSmartPayout(String deviceCode) {
        for (SmartPayout sp : getSmartPayoutInventory()) {
            if (sp.getDeviceCode().equalsIgnoreCase(deviceCode)) {
                String sql = "update ssp_inventory set current_payout_note = ?, current_cashbox_note = ? "
                        + " where device_code = ? and denom = ?";
                Object[] param = new Object[]{0, 0, deviceCode, sp.getDenom()};
                jdbcTemplate.update(sql, param);
                sql = "insert into ssp_history (device_code, denom, stan, command, payout, cashbox) values (?,?,?,?,?,?)";
                param = new Object[]{deviceCode, sp.getDenom(), null, "clear", 0, -sp.getCurrentCashboxNote()};
                jdbcTemplate.update(sql, param);
            }
        }
    }

    @Override
    public void floatSmartPayout(String deviceCode, int denom, int CurrentPayoutNote) {
    }

    @Override
    public void updateDeviceStatus(String deviceCode, String deviceStatusCode) {
        String sql = "update device set device_status = ? where device_code = ?";
        Object[] param = new Object[]{deviceStatusCode, deviceCode};
        jdbcTemplate.update(sql, param);
    }

    @Override
    public void updateCurrentRouting(String deviceCode, int denom, int routing) {
        String sql = "update ssp_inventory set current_routing = ?  where device_code = ? and denom = ?";
        Object[] param = new Object[]{routing, deviceCode, denom};
        jdbcTemplate.update(sql, param);
    }

    @Override
    public String getDeviceStatus(String deviceCode) {
        String sql = "select device_status from device where device_code = ?";
        Object[] param = new Object[]{deviceCode};
        return (String) jdbcTemplate.queryForObject(sql, param, String.class);
    }

    @Override
    public List<Device> getDeviceStatus() {
        String sql = "select device_code, device_status from device";
        return jdbcTemplate.query(sql, new DeviceRowMapper());
    }

    @Override
    public List<CardDispenser> getCardDispenserInventory() {
        String sql = "select device_code, current_amount, max_amount from card_dispenser_inventory";
        return jdbcTemplate.query(sql, new CardDispenserRowMapper());
    }

    @Override
    public void executeSql(String sql) {
        jdbcTemplate.update(sql);
    }

    @Override
    @SuppressWarnings("CallToThreadDumpStack")
    public List<String> getBackupTableData(final String table, String sql, final String kioskId) {
        final List<String> list = new ArrayList<String>();

        try {
            jdbcTemplate.query(sql, new ResultSetExtractor() {

                @Override
                public Object extractData(ResultSet rs) throws SQLException, DataAccessException {

                    int colCount = rs.getMetaData().getColumnCount();

                    String iSQL = "INSERT INTO "+table+" ( ";

                    for (int i = 0; i < colCount; i++) {
                        String colName = rs.getMetaData().getColumnName(i + 1);
                        if (!rs.getMetaData().isAutoIncrement(i + 1)){                            
                            iSQL += colName;
                            if (i < (colCount - 1)) {
                                iSQL += ",";
                            }
                        }
                    }

                    iSQL += ",kiosk_id ) VALUES ( ";


                    while (rs.next()) {
                        StringBuilder o = new StringBuilder();

                        for (int i = 0; i < colCount; i++) {

                            if (!rs.getMetaData().isAutoIncrement(i+1)){
                                int type = rs.getMetaData().getColumnType(i + 1);

                                if (null == rs.getObject(i + 1)) {
                                    o.append("null");
                                } else {
                                    switch (type) {
                                        case Types.SMALLINT:
                                            o.append(rs.getInt(i + 1));
                                            break;
                                        case Types.INTEGER:
                                            o.append(rs.getInt(i + 1));
                                            break;
                                        case Types.BIGINT:
                                            o.append(rs.getLong(i + 1));
                                            break;
                                        case Types.DECIMAL:
                                            o.append(rs.getBigDecimal(i + 1));
                                            break;
                                        case Types.TIMESTAMP:
                                            o.append("'").append(rs.getTimestamp(i + 1)).append("'");
                                            break;
                                        default:
                                            o.append("'").append(rs.getString(i + 1)).append("'");
                                            break;
                                    }
                                }

                                if (i < (colCount - 1)) {
                                    o.append(",");
                                }
                            }
                        }

                        o.append(",'").append(kioskId).append("' ) ");

                        o.insert(0, iSQL);
                        list.add(o.toString());
                    }
                    return null;
                }
            });
        } catch (Exception e) {
            e.printStackTrace();
            return null;
        }
        return list;
    }
}
class SmartPayoutRowMapper implements RowMapper {

    @Override
    public Object mapRow(ResultSet rs, int i) throws SQLException {
        SmartPayout sp = new SmartPayout();
        sp.setDeviceCode(rs.getString("device_code"));
        sp.setIdx(rs.getInt("idx"));
        sp.setDenom(rs.getInt("denom"));        
        sp.setMaxPayoutNote(rs.getInt("max_payout_note"));
        sp.setCurrentPayoutNote(rs.getInt("current_payout_note"));
        sp.setCurrentCashboxNote(rs.getInt("current_cashbox_note"));
        sp.setCurrentRouting(rs.getInt("current_routing"));
        sp.setDescription(rs.getString("description"));
        return sp;
    }
}

class DeviceRowMapper implements RowMapper {

    @Override
    public Object mapRow(ResultSet rs, int i) throws SQLException {
        Device device = new Device();
        device.setDeviceCode(rs.getString("device_code"));
        device.setDeviceStatus(rs.getString("device_status"));
        return device;
    }
}

class CardDispenserRowMapper implements RowMapper{
    @Override
    public Object mapRow(ResultSet rs, int i) throws SQLException {
        CardDispenser cd = new CardDispenser();
        cd.setDeviceCode(rs.getString("device_code"));
        cd.setCurrentAmount(rs.getInt("current_amount"));
        cd.setMaxAmount(rs.getInt("max_amount"));
        return cd;
    }
}