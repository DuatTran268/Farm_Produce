import { get_api } from "./AxiosCommon";

export async function getAllProduct(){
  return get_api (`https://localhost:7047/api/products/getall`)

}

export async function getProductNewestLimit(){
  return get_api (`https://localhost:7047/api/products/limitNewest/4`)

}

export async function getDetailProductByUrlSlug(urlSlug = ''){
  return get_api (`https://localhost:7047/api/products/slugProduct/${urlSlug}`)
}


// get product with slug of category
export function getProductByCategorySlug(slug) {
  return get_api(
    `https://localhost:7047/api/categories/product/slugCategory/${slug}?PageSize=10&PageNumber=1`
  );
}

// export function getProductByCategorySlug(slug) {
//   return get_api(
//     `https://localhost:7047/api/categories/slugCategory/${slug}`
//   );
// }