package com.ics.ssk.ego.util;

import java.util.List;
import org.displaytag.pagination.PaginatedList;
import org.displaytag.properties.SortOrderEnum;

/**
 *
 * @author Hatta Palino
 */
@SuppressWarnings("rawtypes")
public class PaginatedListImpl implements PaginatedList {

    /**
     * Request params as constants.
     */
    public interface IRequestParameters {

        String SORT = "sort";
        String PAGE = "page";
        String ASC = "asc";
        String DESC = "desc";
        String DIRECTION = "dir";
    }
    int DEFAULT_PAGE_SIZE = 20;
    private int index;
    private int pageSize;
    private int fullListSize;
    private List list;
    private SortOrderEnum sortDirection = SortOrderEnum.ASCENDING;
    private String sortCriterion;

    public PaginatedListImpl() {
    }

    public int getFirstRecordIndex() {
        return index * pageSize;
    }

    public int getIndex() {
        return index;
    }

    public void setIndex(int index) {
        this.index = index;
    }

    public int getPageSize() {
        if (pageSize == 0) {
            pageSize = DEFAULT_PAGE_SIZE;
        }
        return pageSize;
    }

    public void setPageSize(int pageSize) {
        this.pageSize = pageSize;
    }

    @Override
    public List getList() {
        return list;
    }

    public void setList(List results) {
        this.list = results;
    }

    @Override
    public int getFullListSize() {
        return fullListSize;
    }

    public void setTotalNumberOfRows(int total) {
        this.fullListSize = total;
    }

    public int getTotalPages() {
        return (int) Math.ceil(((double) fullListSize) / pageSize);
    }

    @Override
    public int getObjectsPerPage() {
        return pageSize;
    }

    @Override
    public int getPageNumber() {
        return index + 1;
    }

    @Override
    public String getSearchId() {
        // Not implemented for now.
        // This is required, if we want the ID to be included in the paginated
        // purpose.
        return null;

    }

    @Override
    public String getSortCriterion() {
        return sortCriterion;
    }

    public void setSortCriterion(String sortCriterion) {
        this.sortCriterion = sortCriterion;
    }

    @Override
    public SortOrderEnum getSortDirection() {
        return sortDirection;
    }

    public void setSortDirection(SortOrderEnum sortDirection) {
        this.sortDirection = sortDirection;
    }
}