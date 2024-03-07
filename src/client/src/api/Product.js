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

