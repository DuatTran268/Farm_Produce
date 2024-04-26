import { get_api, get_api_nocache, post_api, post_api_json, put_api } from "./AxiosCommon";

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
  return post_api_json(`https://localhost:7047/api/account/update-account/order/${id}`, formData);
}


export async function getComboboxPaymentMethod() {
  return get_api(`https://localhost:7047/api/paymentMethods/combobox`);
}

