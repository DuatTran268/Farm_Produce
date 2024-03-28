import { delete_api, get_api, get_api_nocache, put_api } from "./AxiosCommon";


export function getCommentBySlugOfProduct(slug) {
  return get_api(
    `https://localhost:7047/api/products/cmt/slugProduct/${slug}?PageSize=10&PageNumber=1`
  );
}

export async function createNewAndUpdateComment(id = 0, formData) {
  return put_api(`https://localhost:7047/api/comments/${id}`, formData);
}


export function getFilterComment(
  name = '',
  // status = '',
  pageSize = "",
  pageNumber = 1,
  sortColumn = "",
  sortOrder = ""
) {
  let url = new URL(`https://localhost:7047/api/comments/filter`);
  name !== '' && url.searchParams.append('Name', name);
  // status !== '' && url.searchParams.append('Status', status);
  sortColumn !== "" && url.searchParams.append("SortColumn", sortColumn);
  sortOrder !== "" && url.searchParams.append("SortOrder", sortColumn);
  url.searchParams.append("PageSize", pageSize);
  url.searchParams.append("PageNumber", pageNumber);

  return get_api_nocache(url.href);
}


export async function deleteComment(id = 0) {
  return delete_api(`https://localhost:7047/api/comments/${id}`);
}

