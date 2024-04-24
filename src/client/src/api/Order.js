import { get_api_nocache, put_api } from "./AxiosCommon";

export function getOderPagination(
  pageSize = "",
  pageNumber = 1,
  sortColumn = "",
  sortOrder = ""
) {
  let url = new URL(`https://localhost:7047/api/orders`);
  sortColumn !== "" && url.searchParams.append("SortColumn", sortColumn);
  sortOrder !== "" && url.searchParams.append("SortOrder", sortColumn);
  url.searchParams.append("PageSize", pageSize);
  url.searchParams.append("PageNumber", pageNumber);

  return get_api_nocache(url.href);
}


export async function createOrder(id = "", formData) {
  return put_api(`https://localhost:7047/api/account/update-account/order/${id}`, formData);
}




