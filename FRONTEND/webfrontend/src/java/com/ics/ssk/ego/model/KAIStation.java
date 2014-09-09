package com.ics.ssk.ego.model;

public class KAIStation implements java.io.Serializable {

    private static final long serialVersionUID = -2726606446929224957L;
    private String id;
    private KAIStationGroup kaiStationGroup;
    private String description;
    private String original;
    private String destination;

    public KAIStation() {
    }

    public KAIStation(String id, KAIStationGroup kaiStationGroup, String description, String original, String destination) {
        this.id = id;
        this.kaiStationGroup = kaiStationGroup;
        this.description = description;
        this.original = original;
        this.destination = destination;
    }

    public String getId() {
        return this.id;
    }

    public void setId(String id) {
        this.id = id;
    }

    public KAIStationGroup getKaiStationGroup() {
        return this.kaiStationGroup;
    }

    public void setKaiStationGroup(KAIStationGroup kaiStationGroup) {
        this.kaiStationGroup = kaiStationGroup;
    }

    public String getDescription() {
        return this.description;
    }

    public void setDescription(String description) {
        this.description = description;
    }

    public String getOriginal() {
        return this.original;
    }

    public void setOriginal(String original) {
        this.original = original;
    }

    public String getDestination() {
        return this.destination;
    }

    public void setDestination(String destination) {
        this.destination = destination;
    }
}
