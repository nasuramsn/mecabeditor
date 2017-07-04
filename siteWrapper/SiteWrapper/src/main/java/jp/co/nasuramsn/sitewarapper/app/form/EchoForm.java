package jp.co.nasuramsn.sitewarapper.app.form;

import java.io.Serializable;

import javax.validation.constraints.Size;

import org.hibernate.validator.constraints.NotEmpty;

//echo画面用のform
public class EchoForm implements Serializable{

	private static final long serialVersionUID = 1l;

	@NotEmpty
	@Size(max = 100)
	private String text;

	public String getText() {
		return text;
	}

	public void setText(String text) {
		this.text = text;
	}

}
