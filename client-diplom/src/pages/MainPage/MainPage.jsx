import React from 'react'
import "./MainPage.css"
import { Button, Input, Row, Col, Upload, Spin, notification } from 'antd'
import { UploadOutlined } from '@ant-design/icons';
import { useUpload } from '../../hooks/uploadHook';
import { useInputHook } from '../../hooks/useInputHook';
import { postFile } from '../../axios/postFile';
import { CustomSelect } from '../../components/Select/Select';
import { useSelectHook } from '../../hooks/selectHook';
import { useDispatch } from 'react-redux';
import { useSelector } from 'react-redux';
import { setReport } from '../../redux/actions/reportActions';
import Text from 'antd/lib/typography/Text';
import { Link } from 'react-router-dom';
import { useState } from 'react';

const openNotificationWithIcon = (type, data) => {
    notification[type]({
      message: data.message,
      description: data.description,
    });
  };

export const MainPage = () => {

    const { TextArea } = Input;
    const [isLoading, setLoading] = useState(false)
    const uploadHook = useUpload("uploadFile", []);
    const textAreaHook = useInputHook("", "text-user")
    const selectHook = useSelectHook("Метод N-грам", "ngram-check")
    const dispatch = useDispatch()
    const reportSelector = useSelector(r => r.report)
    console.log(isLoading)

    const onClickHandle = () => {
        setLoading(true)
        if (uploadHook.fileList.length !== 0 && uploadHook.fileList[0].status !== "removed") {
            const file = uploadHook.fileList[0];
            postFile(`/CheckDuplicate/file/${selectHook.value}`, file.originFileObj).then(res => {
                dispatch(setReport(res.data.value))
                setLoading(false)
                openNotificationWithIcon('success', {message: "Успішно", description: "Перевірка пройшла успішно"})
            })
        } else if (textAreaHook.value !== "") {
            postFile(`/CheckDuplicate/text/${selectHook.value}`, textAreaHook.value).then(res => {
                dispatch(setReport(res.data.value))
                setLoading(false)
                openNotificationWithIcon('success', {message: "Успішно", description: "Перевірка пройшла успішно"})
            })
        }
    }

    return (
        <>
            <div className="site-layout-content">
                <TextArea rows={10} placeholder="Ваший текст"  {...textAreaHook} className="input-text" />
                <Row gutter={{ xs: 8, sm: 16, md: 24, lg: 32 }} style={{ marginTop: "25px" }}>
                    <Col className="gutter-row" span={4}>
                        <Upload {...uploadHook} >
                            <Button icon={<UploadOutlined />}>Додати файл</Button>
                        </Upload>
                    </Col>
                    <Col className="gutter-row" span={4}>
                        <CustomSelect hook={selectHook} />
                    </Col>
                    <Col className="gutter-row" span={4}>
                        <Button onClick={onClickHandle} type="default" block>
                            Перевірити
                        </Button>
                    </Col>
                    {
                        isLoading ?
                            <Spin /> :
                            reportSelector.results.length != 0 ?
                            <>
                                <Col className="gutter-row" span={4}>
                                    <Text className='result' type={parseInt(reportSelector.maxDuplicate) < 25 ?
                                        "success" : parseInt(reportSelector.maxDuplicate) > 25 && parseInt(reportSelector.maxDuplicate) < 50
                                            ? "warning"
                                            : "danger"} >{parseInt(reportSelector.maxDuplicate)}%</Text>
                                </Col>
                                <Col className="gutter-row" span={4}>
                                    <Link className='result' to="/report">Детальніше</Link>
                                </Col>
                            </>
                            :
                            <>
                            </>
                    }
                </Row>
            </div>
        </>
    )
}