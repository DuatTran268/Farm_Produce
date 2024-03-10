import { get_api } from "./AxiosCommon";


export function getCommentBySlugOfProduct(slug) {
  return get_api(
    `https://localhost:7047/api/products/cmt/slugProduct/${slug}?PageSize=10&PageNumber=1`
  );
}