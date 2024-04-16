import { delete_api, get_api_nocache, post_api } from "./AxiosCommon";




export function getAllImagePagination(
  pageSize = "",
  pageNumber = 1,
  sortColumn = "",
  sortOrder = ""
) {
  let url = new URL(`https://localhost:7047/api/images`);
  sortColumn !== "" && url.searchParams.append("SortColumn", sortColumn);
  sortOrder !== "" && url.searchParams.append("SortOrder", sortColumn);
  url.searchParams.append("PageSize", pageSize);
  url.searchParams.append("PageNumber", pageNumber);

  return get_api_nocache(url.href);
}


export async function deleteImage(id = 0) {
  return delete_api(`https://localhost:7047/api/images/${id}`);
}



export async function getImageById(id = 0) {
  if (id > 0) {
    return get_api_nocache(`https://localhost:7047/api/images/${id}`);
  }
}


export async function createAndUpdateImage( formData) {
  return post_api(`https://localhost:7047/api/images`, formData);
}

export async function getFilterComboboxProduct() {
  return get_api_nocache(`https://localhost:7047/api/products/combobox`);
}
