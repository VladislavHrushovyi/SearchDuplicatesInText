import React from 'react'
import "./MainPage.css"
import { Button, Input, Row, Col, Upload, Progress } from 'antd'
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
import { openNotificationWithIcon } from '../../utils/notification';
import { getProgressChecking } from '../../utils/getCheckingProgress';



export const MainPage = () => {

    const { TextArea } = Input;
    const [isLoading, setLoading] = useState(false)
    const [progressComplete, setProgressComplete] = useState(0)
    const uploadHook = useUpload("uploadFile", []);
    const textAreaHook = useInputHook("", "text-user")
    const selectHook = useSelectHook("Метод N-грам", "ngram-check")
    const dispatch = useDispatch()
    const reportSelector = useSelector(r => r.report)
    const user = useSelector(r => r.user)

    const onClickHandle = async () => {
        console.log(user)
        if(!user.islogged){
            openNotificationWithIcon("warning", {message: "Авторизуйтеся", description:"Увійдіть до системи для того щоб користуватися можливостями сервісу"})
            return
        }
        setLoading(true)
        setProgressComplete(0)
        const progressName = Date.now().toString()

        const interval = getProgressChecking(progressName, user.token, setProgressComplete)

        if (uploadHook.fileList.length !== 0 && uploadHook.fileList[0].status !== "removed") {
            const file = uploadHook.fileList[0];
            postFile(`/CheckDuplicate/file/${selectHook.value}`, file.originFileObj, progressName, user.token).then(res => {
                dispatch(setReport(res.data))
                setLoading(false)
                openNotificationWithIcon('success', { message: "Успішно", description: "Перевірка пройшла успішно" })
                clearInterval(interval)
                return
            })
        } else if (textAreaHook.value !== "") {
            postFile(`/CheckDuplicate/text/${selectHook.value}`, textAreaHook.value, progressName, user.token).then(res => {
                dispatch(setReport(res.data))
                setLoading(false)
                openNotificationWithIcon('success', { message: "Успішно", description: "Перевірка пройшла успішно" })
                clearInterval(interval)
                return
            })
        } else {
            openNotificationWithIcon('warning', { message: "Попередження", description: "Ви забули додати текст або файл" })
            setLoading(false)
            return
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
                        <Progress  type="circle" percent={parseInt(progressComplete)} /> :
                    
                            reportSelector.results.length !== 0  ?
                                <>
                                    <Col className="gutter-row" span={4}>
                                        <Text className='result' type={parseInt(reportSelector.maxDuplicate) < 25 ?
                                            "success" : parseInt(reportSelector.maxDuplicate) > 25 && parseInt(reportSelector.maxDuplicate) < 50
                                                ? "warning"
                                                : "danger"} >Схожість {parseInt(reportSelector.maxDuplicate)}%</Text>
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