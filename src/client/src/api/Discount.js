import { get_api_nocache } from "./AxiosCommon";


export function getVoucherDiscountPagination(
  pageSize = "",
  pageNumber = 1,
  sortColumn = "",
  sortOrder = ""
) {
  let url = new URL(`https://localhost:7047/api/discount/getall`);
  sortColumn !== "" && url.searchParams.append("SortColumn", sortColumn);
  sortOrder !== "" && url.searchParams.append("SortOrder", sortColumn);
  url.searchParams.append("PageSize", pageSize);
  url.searchParams.append("PageNumber", pageNumber);

  return get_api_nocache(url.href);
}
