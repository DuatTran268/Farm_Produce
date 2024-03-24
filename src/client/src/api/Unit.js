import { get_api } from "./AxiosCommon";

export function getFilterUnit(
  name = '',
  pageSize = "",
  pageNumber = 1,
  sortColumn = "",
  sortOrder = ""
) {
  let url = new URL(`https://localhost:7047/api/units/pagination`);
  name !== '' && url.searchParams.append('Name', name);
  sortColumn !== "" && url.searchParams.append("SortColumn", sortColumn);
  sortOrder !== "" && url.searchParams.append("SortOrder", sortColumn);
  url.searchParams.append("PageSize", pageSize);
  url.searchParams.append("PageNumber", pageNumber);

  return get_api(url.href);
}

// get unit by id
export async function getUnitById(id = 0) {
  if (id > 0) {
    return get_api(`https://localhost:7047/api/units/${id}`);
  }
}

