import { delete_api, get_api_nocache, put_api } from "./AxiosCommon";


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


export async function getDiscountById(id = 0) {
  if (id > 0) {
    return get_api_nocache(`https://localhost:7047/api/discount/${id}`);
  }
}


export async function newCreateAndUpdateDiscount(id = 0, formData) {
  return put_api(`https://localhost:7047/api/discount/${id}`, formData);
}


export async function deleteVoucherDiscount(id = 0) {
  return delete_api(`https://localhost:7047/api/discount/${id}`);
}

