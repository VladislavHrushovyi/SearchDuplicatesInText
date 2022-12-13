import { ax } from "./axios";

export const uploadFile = async (path, data, token) => {
    let formData = new FormData();
    formData.append("file", data)
    return await ax.post(path, formData, {
        headers: {
            'Content-Type': 'multipart/form-data',
            'Authorization': "Bearer " + token
        }
    })
}