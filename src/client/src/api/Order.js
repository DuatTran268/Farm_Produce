import { delete_api, get_api, get_api_nocache, post_api_json, put_api, put_api_json } from "./AxiosCommon";

export function getOderPagination(
  id = "",
  name = "",
  pageSize = "",
  pageNumber = 1,
  sortColumn = "",
  sortOrder = ""
) {
  let url = new URL(`https://localhost:7047/api/orders`);
  id !== '' && url.searchParams.append('Id', id);
  name !== '' && url.searchParams.append('Name', name);
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

export async function getComboboxStatusOrder() {
  return get_api_nocache(`https://localhost:7047/api/oderstatus/combobox`);
}

export async function deleteOrder(id = 0) {
  return delete_api(`https://localhost:7047/api/orders/${id}`);
}


export async function getInforOfVoucherDiscount(codeName = "") {
  return get_api_nocache(`https://localhost:7047/api/discount/${codeName}`);
}



export async function getOrderById(id = 0) {
  if (id > 0) {
    return get_api_nocache(`https://localhost:7047/api/orders/${id}`);
  }
}


export async function UpdateInforOrder(formData) {
  return put_api_json('https://localhost:7047/api/orders/update-order', formData);
}
