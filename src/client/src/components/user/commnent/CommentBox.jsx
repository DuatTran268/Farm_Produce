import React from "react";
import { Button } from "react-bootstrap";

const CommentBox = () => {
  return (
    <section>
      <div className="comment_content">
        <h5 className="comment_title">Để lại bình luận của bạn</h5>
      </div>
      <div className="comment_box">
        <div className="card-body">
          <form>
            {/* <form ref={form} onSubmit={sendEmail}> */}
            <div className="form-group">
              <input
                id="name"
                type="text"
                name="user_name"
                className="form-control"
                placeholder="Nhập tên của bạn"
                required
              ></input>
            </div>
            <div className="form-group py-3">
              <input
                id="email"
                type="email"
                className="form-control"
                name="user_email"
                placeholder="Nhập Email của bạn"
                required
              ></input>
            </div>
            <div className="form-group">
              <textarea
                id="content"
                type="text"
                rows={6}
                className="form-control"
                name="text_message"
                placeholder="Nhập nội dung"
                required
              ></textarea>
            </div>
            <br />
            <div className="justify-content-center d-flex">
              <Button type="submit" value="Send" className="btn btn-primary">
                Gửi phản hồi
              </Button>
            </div>
          </form>
        </div>
      </div>
    </section>
  );
};
export default CommentBox;
