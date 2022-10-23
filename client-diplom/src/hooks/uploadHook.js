import { useState } from "react"

export const useUpload = (name, init) => {
    const [file, setFile] = useState(init);
    return {
        name: name,
        customRequest: ({ file, onSuccess }) => {
            onSuccess("ok")
        },
        onChange: (response) => {
            if(response.status !== "removed"){
                setFile([response.file])
            }
            if(response.file.status === "removed"){
                response.file = {}
                setFile([])
            }
        },
        fileList: file,
    }
}