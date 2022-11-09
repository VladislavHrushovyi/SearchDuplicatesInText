import { ax } from "./axios";

export const postFile = async (path, data, progressName, token) => {
    let formData = new FormData();
    formData.append("data", data)
    formData.append("progressName", progressName)
    console.log(token)
    return await ax.post(path, formData, {
        headers: {
            'Content-Type': 'multipart/form-data',
            'Authorization': "Bearer " + token
        }
    })
}
