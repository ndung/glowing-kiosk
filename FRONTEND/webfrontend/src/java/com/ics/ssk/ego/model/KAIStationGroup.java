package com.ics.ssk.ego.model;

import java.util.Set;
import java.util.TreeSet;

public class KAIStationGroup implements java.io.Serializable {

    private static final long serialVersionUID = 4300878562667859287L;
    private String id;
    private String description;
    private Set<KAIStation> kaiStations = new TreeSet<KAIStation>();

    public KAIStationGroup() {
    }

    public KAIStationGroup(String id, String description) {
        this.id = id;
        this.description = description;
    }

    public KAIStationGroup(String id, String description, Set<KAIStation> kaiStations) {
        this.id = id;
        this.description = description;
        this.kaiStations = kaiStations;
    }

    public String getId() {
        return this.id;
    }

    public void setId(String id) {
        this.id = id;
    }

    public String getDescription() {
        return this.description;
    }

    public void setDescription(String description) {
        this.description = description;
    }

    public Set<KAIStation> getKaiStations() {
        return this.kaiStations;
    }

    public void setKaiStations(Set<KAIStation> kaiStations) {
        this.kaiStations = kaiStations;
    }
}
