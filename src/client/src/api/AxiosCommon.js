import axios from "axios";


const apiCache = {}; // Đối tượng lưu trữ kết quả của các yêu cầu API đã được gọi trước đó

export async function get_api(your_api) {
  try {
    // Kiểm tra xem kết quả của yêu cầu API đã được lưu trong cache chưa
    if (apiCache[your_api]) {
      return apiCache[your_api];
    } else {
      const response = await axios.get(your_api);
      const data = response.data;
      if (data.isSuccess) {
        // Lưu kết quả vào cache trước khi trả về
        apiCache[your_api] = data.result;
        return data.result;
      } else {
        return null;
      }
    }
  } catch (error) {
    console.log("Error ", error.message);
    return null;
  }
}


export async function get_api_nocache(your_api) {
  try {
    const response = await axios.get(your_api);
    const data = response.data;
    if (data.isSuccess) return data.result;
    else return null;
  } catch (error) {
    console.log("Error ", error.message);
    return null;
  }
}


export async function post_api(your_api, formData) {
  try {
    const response = await axios.post(your_api, formData);
    const data = response.data;
    console.log("postdata", data);
    if (data.isSuccess)
      return data.result;
    else
      return null;
  } catch (error) {
    console.log('Error', error.message);
    return null;
  }
}
export async function post_api_json(your_api, data) {
  try {
    const response = await axios.post(your_api, data, {
      headers: {
        'Content-Type': 'application/json'
      }
    });
    const responseData = response.data;
    console.log("postdata", responseData);
    if (responseData.isSuccess)
      return responseData.result;
    else
      return null;
  } catch (error) {
    console.log('Error', error.message);
    return null;
  }
}

export async function put_api_json(your_api, data) {
  try {
    const response = await axios.put(your_api, data, {
      headers: {
        'Content-Type': 'application/json'
      }
    });
    const responseData = response.data;
    console.log("postdata", responseData);
    if (responseData.isSuccess)
      return responseData.result;
    else
      return null;
  } catch (error) {
    console.log('Error', error.message);
    return null;
  }
}


export async function put_api(your_api, formData){
 
  try {
    let formDataObject = Object.fromEntries(formData.entries());
    // Format the plain form data as JSON
    let formDataJsonString = JSON.stringify(formDataObject);

    const response = await axios({
      method: 'put',
      url: your_api,
      data: formDataJsonString,
      headers: {
        accept: 'application/json',
        'Content-Type': 'application/json'
      }
    })

    const data = response.data;
    if (data.isSuccess){
      return data.result;
    }
    else{
      return null;
    }
  } catch (error) {
    console.log("Error ", error.message);
    return null;
  }
}


export async function delete_api(your_api){
  try {
    const response = await axios.delete(your_api);

    const data = response.data;
    if (data.isSuccess){
      return data.result;
    }
    else{
      return null;
    }

  } catch (error) {
    console.log("Error ", error.message);
    return null;
  }
}

