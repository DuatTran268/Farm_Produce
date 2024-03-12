import { get_api } from "./AxiosCommon";



export async function getCategoryLimit(){
  return get_api (`https://localhost:7047/api/categories/limit/4`)

}