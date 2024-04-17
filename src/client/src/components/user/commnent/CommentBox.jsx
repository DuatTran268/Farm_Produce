import React, { useEffect, useState } from "react";
import { Button, Form } from "react-bootstrap";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faMessage, faStar } from "@fortawesome/free-solid-svg-icons";
import { faStar as faStarRegular } from "@fortawesome/free-regular-svg-icons";
import { useNavigate, useParams } from "react-router-dom";
import { useSnackbar } from "notistack";
import { createNewAndUpdateComment } from "../../../api/Comment";
import FieldComment from "../../admin/edit/FieldBox";
import { getDetailProductByUrlSlug, getIdAndSlugOfProductForComment } from "../../../api/Product";
import { useSelector } from "react-redux";
import "./Comment.css"


const CommentBox = () => {
  const { enqueueSnackbar } = useSnackbar();
  const [validated, setValidated] = useState(false);
  const initialState = {
    id: 0,
    name: "",
    rating: 0,
    created: "",
    commentText: "",
    status: false,
    applicationUserId: "",
    productId: 0,
  };
  const [comment, setComment] = useState(initialState);
  const navigate = useNavigate();
  const { slug } = useParams();
  const productDetail = useSelector(state => state.productDetail);
  let { id } = useParams();
  id = id ?? 0;
  useEffect(() => {
    document.title = "Chi tiết sản phẩm";
    // get id and slug of product
    getIdAndSlugOfProductForComment(slug).then((data) => {
      if (data) {
        console.log("Product detail:", data.id);
        setComment(prevState => ({ ...prevState, productId: data.id }));
      } else {
        console.log("Product not found");
      }
    });
  }, [slug]);
  let user = useSelector((state) => state.auth.login.currentUser);

  useEffect(() => {
    console.log('Check id của user:  ', user.id)
    if (user.id) {
      setComment(prevState => ({ ...prevState, applicationUserId: user.id }));
    }
  }, []);

  const handleSubmit = (e) => {
    e.preventDefault();
    if (e.currentTarget.checkValidity() === false) {
      e.stopPropagation();
      setValidated(true);
    } else {
      let data = new FormData(e.target);
      data.append("rating", comment.rating); // Thêm rating vào formData
      
      console.log("Form data:");
      for (const pair of data.entries()) {
        console.log(pair[0] + ': ' + pair[1]);
      }

      createNewAndUpdateComment(id, data).then((data) => {
        if (data) {
          enqueueSnackbar("Cảm ơn bạn đã bình luận", {
            variant: "success",
          });
          navigate(`/detail/${slug}`);
        } else {
          enqueueSnackbar("Đã xảy ra lỗi bình luận", {
            variant: "error",
          });
        }
      });
    }
  };

  return (
    <section>
      <div className="comment_content">
        <h5 className="comment_title">Để lại bình luận của bạn</h5>
      </div>
      <div className="comment_box">
        <Form
          method="post"
          encType="multipart/form-data"
          onSubmit={handleSubmit}
          noValidate
          validated={validated}
        >
          <Form.Control type="hidden" name="id" value={comment.id} />

          <div className="mb-2 px-3">
            <span>Đánh giá:</span>{" "}
            {[1, 2, 3, 4, 5].map((value) => (
              <FontAwesomeIcon
                key={value}
                icon={value <= comment.rating ? faStar : faStarRegular}
                onClick={() => setComment({ ...comment, rating: value })} // Thay đổi rating trong state
                className="px-1 icon_start"
                style={{ cursor: "pointer" }}
              />
            ))}
          </div>

          <FieldComment
            control={
              <Form.Control
                className="form_control_comment"
                placeholder="Tên hiển thị"
                type="text"
                name="name"
                title="Name"
                required
                value={comment.name}
                onChange={(e) =>
                  setComment({ ...comment, name: e.target.value })
                }
              />
            }
            notempty={"Không được bỏ trống"}
          />

          <FieldComment
            control={
              <Form.Control
                className="form_control_comment"
                placeholder="Nội dung bình luận"
                type="text"
                name="commentText"
                title="Comment Text"
                required
                value={comment.commentText}
                onChange={(e) =>
                  setComment({ ...comment, commentText: e.target.value })
                }
              />
            }
            notempty={"Không được bỏ trống"}
          />

          <Form.Control type="hidden" name="applicationUserId" value={comment.applicationUserId} />
          <Form.Control type="hidden" name="productId" value={comment.productId} />

          <div className="text-center">
            <Button variant="success" type="submit">
              Bình luận
              <FontAwesomeIcon icon={faMessage} className="px-2" />
            </Button>
          </div>
        </Form>
      </div>
    </section>
  );
};

export default CommentBox;
