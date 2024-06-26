import { delete_api, get_api, get_api_nocache, post_api, put_api } from "./AxiosCommon";

export async function getAllAccount(){
  return get_api (`https://localhost:7047/api/account`)

}


export function getFilterProduct(
  name = '',
  status = '',
  pageSize = "",
  pageNumber = 1,
  sortColumn = "",
  sortOrder = ""
) {
  let url = new URL(`https://localhost:7047/api/products/getall`);
  name !== '' && url.searchParams.append('Name', name);
  status !== '' && url.searchParams.append('Status', status);
  sortColumn !== "" && url.searchParams.append("SortColumn", sortColumn);
  sortOrder !== "" && url.searchParams.append("SortOrder", sortOrder);
  url.searchParams.append("PageSize", pageSize);
  url.searchParams.append("PageNumber", pageNumber);

  return get_api_nocache(url.href);
}



export async function getProductNewestLimit(){
  return get_api (`https://localhost:7047/api/products/limitNewest/5`)

}

export async function getDetailProductByUrlSlug(urlSlug = ''){
  return get_api_nocache (`https://localhost:7047/api/products/slugProduct/${urlSlug}`)
}

export async function getIdAndSlugOfProductForComment(urlSlug = ''){
  return get_api (`https://localhost:7047/api/products/slugidProduct/${urlSlug}`)
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

export async function getProductById(id = 0) {
  if (id > 0) {
    return get_api_nocache(`https://localhost:7047/api/products/${id}`);
  }
}


export async function newAndUpdateProduct(formData) {
  return post_api('https://localhost:7047/api/products/', formData);
}


export async function deletProduct(id = 0) {
  return delete_api(`https://localhost:7047/api/products/${id}`);
}


export async function getFilterComboboxOfCategory() {
  return get_api_nocache(`https://localhost:7047/api/categories/combobox`);
}


export async function getFilterComboboxOfUnit() {
  return get_api_nocache(`https://localhost:7047/api/units/combobox`);
}

// increse view count product
export function increaseView(slug){
  return post_api(`https://localhost:7047/api/products/viewCount/${slug}`)
}

