import { get_api } from "./AxiosCommon";

export async function getAllDashboard(){
  return get_api (`https://localhost:7047/api/dashboard`)
}