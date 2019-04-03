namespace Xzy.EmbeddedApp.Model
{
    public enum TaskType
    {
        //[Description("导入通讯录")]
        /// <summary>
        /// 导入通讯录
        /// </summary>
        ImportContacts = 1,

        /// <summary>
        /// 发纯文本动态
        /// </summary>
        PostMessage = 2,

        /// <summary>
        /// 发纯图片动态
        /// </summary>
        PostPicture = 3,

        /// <summary>
        /// 发图文动态
        /// </summary>
        PostMessageAndPicture = 4,

        /// <summary>
        /// 发纯文本消息
        /// </summary>
        SendMessage = 5,

        /// <summary>
        /// 发纯图片消息
        /// </summary>
        SendPicture = 6,

        /// <summary>
        /// 发图文消息
        /// </summary>
        SendMessageAndPicture = 7,

        /// <summary>
        /// 发纯视频消息
        /// </summary>
        SendVideo = 8,

        /// <summary>
        /// 发视文消息
        /// </summary>
        SendMessageAndVideo = 9,

        /// <summary>
        /// 发音频消息
        /// </summary>
        SendAudio = 10,

        /// <summary>
        /// 发送短信验证码
        /// </summary>
        SendVerificationCode = 11,

        /// <summary>
        /// 修改昵称和个性说明
        /// </summary>
        UpdateNickName = 12,


        /// <summary>
        /// 清除通讯录
        /// </summary>
        ClearContact = 13,

        /// <summary>
        /// 创建群组
        /// </summary>
        CreateGroup = 14,

        /// <summary>
        /// 发送群组文本消息
        /// </summary>
        SendGroupTextMessage = 15,

        /// <summary>
        /// 发送群组图片消息
        /// </summary>
        SendGroupPictureMessage = 16,

        /// <summary>
        /// 发送群组视频消息
        /// </summary>
        SendGroupVideoMessage = 17,

    }
}
