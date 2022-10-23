import { ax } from "./axios";

export const postFile = (path, data) => {
    let formData = new FormData();
    formData.append("data", data)

    return ax.post(path, formData, {
        headers: {
            'Content-Type': 'multipart/form-data',
        }
    })
}
