package com.ics.ssk.ego.manager;

import com.ics.ssk.ego.util.PaginatedListImpl;
import com.ics.ssk.ego.util.ParameterDao;
import java.util.List;
import org.displaytag.pagination.PaginatedList;

@SuppressWarnings("rawtypes")
public interface BaseManager {

    public PaginatedList getList(ParameterDao parameter, PaginatedListImpl paginated);

    public PaginatedList getList(ParameterDao parameter, PaginatedListImpl paginated, String ascOrder, String descOrder);

    public List getList(ParameterDao parameter);
}
