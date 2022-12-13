import { Button, Col, Input, Modal, Row, Upload } from "antd";
import "./SettingsModal.css"
import React, { useState } from "react";
import { UploadOutlined } from '@ant-design/icons';
import { useUpload } from "../../hooks/uploadHook";
import { useInputHook } from "../../hooks/useInputHook";
import { useSelector } from "react-redux";
import { uploadFile } from "../../axios/uploadFile";
import { openNotificationWithIcon } from "../../utils/notification";
import { ax } from "../../axios/axios";

export const SettingsModal = ({ open, setOpen }) => {
    const [expPart, setExpPart] = useState(3)
    const [confirmLoading, setConfirmLoading] = useState(false);
    const uploadHook = useUpload("uploadFileOnServer", []);
    let inputHook = useInputHook(expPart + "", "numberOfExpPart")
    const user = useSelector(r => r.user)

    console.log(inputHook)
    
    const handleOk = () => {
        setConfirmLoading(true);
        setTimeout(() => {
            setOpen(false);
            setConfirmLoading(false);
        }, 2000);
    };
    const handleCancel = () => {
        console.log('Clicked cancel button');
        setOpen(false);
    };

    const uploadFileHandle = () => {
        const file = uploadHook.fileList[0].originFileObj
        uploadFile("/Text/upload-file", file, user.token)
            .then(res => {
                const response = `\n Ngram  ${res.data.ngramFile.name} \n Shingle  ${res.data.shingleFile.name} \n Exp  ${res.data.expFile.name} \n`;
                console.log(response)
                openNotificationWithIcon('success', { message: "Успішно", description: `Файл доданий,${response} ` })
            })
    }

    const changeSettingsHandle = () => {
        ax.post("/Settings/update-settings", {expPart: parseInt(inputHook.value)}, {headers:{
            'Authorization': "Bearer " + user.token
        }})
        .then(res => {
            console.log(res.data)
            const response = `ShinglePart = ${res.data.shinglePart} \n NgramPart = ${res.data.ngramPart} \n ExpPart = ${res.data.expPart}`
            setExpPart(res.data.expPart)
            openNotificationWithIcon('success', { message: "Успішно", description: `Змінено на \n ${response} ` })
        })
    }

    return (
        <>
            <Modal
                title="Налаштування"
                open={open}
                visible={open}
                onOk={handleOk}
                confirmLoading={confirmLoading}
                onCancel={handleCancel}
            >
                <Row className="item-container">
                    <Col span={8}>
                    <Upload {...uploadHook} >
                            <Button icon={<UploadOutlined />}>Додати файл</Button>
                        </Upload>
                    </Col>
                    <Col span={6}>
                        <Button onClick={uploadFileHandle}>Відправити файл</Button>
                    </Col>
                </Row>
                <Row className="item-container">
                    <Col span={8} >
                        <Input {...inputHook} className="exp-input"/>
                    </Col>
                    <Col span={6}>
                        <Button onClick={changeSettingsHandle}>Змінити кількість</Button>
                    </Col>
                </Row>
            </Modal>
        </>
    )
}