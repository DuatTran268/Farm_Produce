import { get_api } from "./AxiosCommon";



export async function getProductNewestLimit(){
  return get_api (`https://localhost:7047/api/products/limitNewest/4`)

}