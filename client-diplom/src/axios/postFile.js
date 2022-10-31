import { ax } from "./axios";

export const postFile = async (path, data, progressName) => {
    let formData = new FormData();
    formData.append("data", data)
    formData.append("progressName", progressName)

    return await ax.post(path, formData, {
        headers: {
            'Content-Type': 'multipart/form-data',
        }
    })
}
