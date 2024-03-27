import { get_api, put_api } from "./AxiosCommon";


export function getCommentBySlugOfProduct(slug) {
  return get_api(
    `https://localhost:7047/api/products/cmt/slugProduct/${slug}?PageSize=10&PageNumber=1`
  );
}

export async function createNewAndUpdateComment(id = 0, formData) {
  return put_api(`https://localhost:7047/api/comments/${id}`, formData);
}
