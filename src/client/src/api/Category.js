import { delete_api, get_api, get_api_nocache, post_api } from "./AxiosCommon";



export async function getCategoryLimit(){
  return get_api (`https://localhost:7047/api/categories/limit/6`)

}


export async function getCategoryBySlugOfItSelf(urlSlug = ''){
  return get_api (`https://localhost:7047/api/categories/slugCategory/${urlSlug}`)
}


export function getFilterCategory(
  name = '',
  pageSize = "",
  pageNumber = 1,
  sortColumn = "",
  sortOrder = ""
) {
  let url = new URL(`https://localhost:7047/api/categories`);
  name !== '' && url.searchParams.append('Name', name);
  sortColumn !== "" && url.searchParams.append("SortColumn", sortColumn);
  sortOrder !== "" && url.searchParams.append("SortOrder", sortColumn);
  url.searchParams.append("PageSize", pageSize);
  url.searchParams.append("PageNumber", pageNumber);

  return get_api_nocache(url.href);
}



export async function deletCategory(id = 0) {
  return delete_api(`https://localhost:7047/api/categories/${id}`);
}



export async function getCategoryById(id = 0) {
  if (id > 0) {
    return get_api_nocache(`https://localhost:7047/api/categories/${id}`);
  }
}


export async function createAndUpdateCategory( formData) {
  return post_api(`https://localhost:7047/api/categories`, formData);
}