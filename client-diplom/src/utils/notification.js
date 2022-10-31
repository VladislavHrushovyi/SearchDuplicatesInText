import {notification} from "antd"

export const openNotificationWithIcon = (type, data) => {
    notification[type]({
      message: data.message,
      description: data.description,
    });
  };